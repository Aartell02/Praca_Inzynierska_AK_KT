using GameSystems;
using GameSystems.Config;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Controllers
{
	public class GameplaySceneController : MonoBehaviour
	{
		[SerializeField] GameObject _Player;
		[SerializeField] GameObject _Enemies;
		GameObjectReferences gameObjectReferences;
		EnemySpawnConfig enemySpawnConfig;

		(int, int) AltarsCaptured;

		public IEnumerator Start()
		{
			yield return null;

			yield return WaitForNavMesh();

			gameObjectReferences = GameObjectReferences.Instance;
			enemySpawnConfig = ConfigReferences.Instance.enemyConfig;

			SpawnPlayer();
			SpawnEnemies();
			SpawnAltars();
		}

		public void SpawnPlayer()
		{

			GameObject playerPrefab = gameObjectReferences.Player;

			GameObject player = Instantiate(playerPrefab, GameSystemsViewModel.GetPlayerSpawnPoint(), Quaternion.identity, _Player.transform);
		}
		public void SpawnEnemies()
		{
			var enemies = enemySpawnConfig.EnemySpawnData;
			for (int i = 0; i< enemies.Length; i++)
			{
				GameObject enemyPrefab = gameObjectReferences.Enemy[i];
				for (int j = 0; j < enemies[i].Count; j++)
				{
					GameObject enemy = Instantiate(enemyPrefab, GameSystemsViewModel.GetEnemySpawnPoint() + new Vector2(0,-j), Quaternion.identity, _Enemies.transform);
				}
			}
		}

		public void SpawnAltars()
		{
			AltarsCaptured.Item1 = 0;

			//Altar spawn logic

			//AltarsCaptured.Item2 = count;
		}

		private IEnumerator WaitForNavMesh()
		{
			// Czekamy aż NavMesh będzie gotowy
			while (!NavMesh.SamplePosition(transform.position, out _, 1f, NavMesh.AllAreas))
				yield return null;
		}
	}
}
