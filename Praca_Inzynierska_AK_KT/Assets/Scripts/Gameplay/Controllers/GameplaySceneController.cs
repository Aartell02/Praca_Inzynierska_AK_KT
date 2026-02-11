using Boot;
using GameSystems;
using GameSystems.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Controllers
{
	public class GameplaySceneController : MonoBehaviour
	{
		[SerializeField] GameObject _Player;
		[SerializeField] GameObject _Enemies;
		PrefabReferences gameObjectReferences;
		EnemySpawnConfig enemySpawnConfig;

		public IEnumerator Start()
		{
			yield return null;

			yield return WaitForNavMesh();

			gameObjectReferences = PrefabReferences.Instance;
			enemySpawnConfig = ConfigReferences.Instance.enemyConfig;

			SpawnEnemies();
			SpawnPlayer();
		}
		private void Update()
		{
			if(PlayerStats.Instance.Health <= 0)
			{
				BootViewModel.FinishGame(false);
			}
		}
		public void SpawnPlayer()
		{

			GameObject playerPrefab = gameObjectReferences.Player;

			GameObject player = Instantiate(playerPrefab, GameSystemsViewModel.GetPlayerSpawnPoint(), Quaternion.identity, _Player.transform);
		}

		public void SpawnEnemies()
		{
			// Konfiguracja odstępów między wrogami (np. 1.0f lub 1.5f metra)
			float spacing = 1.5f;

			// Pobieramy iterator spirali (nieskończona lista współrzędnych: (0,0), (1,0), (1,1)...)
			var spiralIterator = GetSpiralCoordinates().GetEnumerator();

			Vector3 spawnCenter = GameSystemsViewModel.GetEnemySpawnPoint();
			var enemies = enemySpawnConfig.EnemySpawnData;

			// Przechodzimy przez każdy typ wroga
			for (int i = 0; i < enemies.Length; i++)
			{
				GameObject enemyPrefab = gameObjectReferences.Enemy[i];
				int enemiesToSpawn = enemies[i].Count;
				int spawnedCount = 0;

				// Zabezpieczenie przed nieskończoną pętlą, gdyby cała mapa była zablokowana
				// Jeśli sprawdzimy 1000 punktów pod rząd i żaden nie będzie dobry - przerywamy.
				int consecutiveFailures = 0;
				int maxConsecutiveFailures = 1000;

				while (spawnedCount < enemiesToSpawn && consecutiveFailures < maxConsecutiveFailures)
				{
					// 1. Pobierz kolejny punkt ze spirali
					// Uwaga: spiralIterator.MoveNext() pamięta stan między pętlami wrogów,
					// więc kolejny typ wroga zacznie tam, gdzie skończył poprzedni (tworząc jedną dużą grupę).
					if (!spiralIterator.MoveNext()) break;

					Vector2 gridPos = spiralIterator.Current;

					// 2. Przelicz na pozycję w świecie (z uwzględnieniem spacingu)
					Vector2 offset = gridPos * spacing;
					Vector3 targetPosition = spawnCenter + new Vector3(offset.x, offset.y, 0);

					// 3. Sprawdź NavMesh
					// Zwiększyłem lekko promień (0.4f), żeby łatwiej łapać navmesh
					if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 0.4f, NavMesh.AllAreas))
					{
						// Znaleziono miejsce -> Spawn
						Instantiate(enemyPrefab, hit.position, Quaternion.identity, _Enemies.transform);
						spawnedCount++;
						consecutiveFailures = 0; // Reset licznika błędów
					}
					else
					{
						// Brak miejsca -> Licznik w górę, pętla leci dalej i bierze KOLEJNY punkt spirali
						// dla TEGO SAMEGO przeciwnika (nie tracimy go).
						consecutiveFailures++;
					}
				}

				if (consecutiveFailures >= maxConsecutiveFailures)
				{
					Debug.LogWarning($" Nie znaleziono miejsca dla wszystkich wrogów typu {i}. Mapa może być zbyt ciasna.");
				}
			}
		}

		/// <summary>
		/// Generator zwracający kolejne współrzędne spirali: (0,0), (1,0), (1,1), (0,1), (-1,1)...
		/// </summary>
		private IEnumerable<Vector2> GetSpiralCoordinates()
		{
			int x = 0;
			int y = 0;
			int steps = 1;

			// Pierwszy punkt (środek)
			yield return new Vector2(x, y);

			while (true)
			{
				// Ruch w Prawo
				for (int i = 0; i < steps; i++) { x++; yield return new Vector2(x, y); }
				// Ruch w Górę
				for (int i = 0; i < steps; i++) { y++; yield return new Vector2(x, y); }

				steps++; // Zwiększamy długość boku spirali

				// Ruch w Lewo
				for (int i = 0; i < steps; i++) { x--; yield return new Vector2(x, y); }
				// Ruch w Dół
				for (int i = 0; i < steps; i++) { y--; yield return new Vector2(x, y); }

				steps++; // Zwiększamy długość boku spirali
			}
		}
		private IEnumerator WaitForNavMesh()
		{
			while (!NavMesh.SamplePosition(transform.position, out _, 1f, NavMesh.AllAreas))
				yield return null;
		}
	}
}
