using Core;
using UnityEngine;

namespace Gameplay.Controllers
{
	public class GameplaySceneController : MonoBehaviour
	{
		public GameObject _Player;
		public GameObject _Enemies;
		GameObjectReferences gameObjectReferences;
		EnemySpawnConfig enemySpawnConfig;
		public void Awake()
		{
			gameObjectReferences = GameObjectReferences.Instance;
			enemySpawnConfig = ConfigReferences.Instance.enemyConfig;
			SpawnPlayer();
			SpawnEnemies();
		}

		public void SpawnPlayer()
		{
			GameObject playerPrefab = gameObjectReferences.Player;

			GameObject player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity, _Player.transform);
		}
		public void SpawnEnemies()
		{
			var enemies = enemySpawnConfig.EnemySpawnData;
			for (int i = 0; i< enemies.Length; i++)
			{
				GameObject enemyPrefab = gameObjectReferences.Enemy[i];
				for (int j = 0; j < enemies[i].Count; j++)
				{
					GameObject enemy = Instantiate(enemyPrefab, Vector2.zero + new Vector2(15,j), Quaternion.identity, _Enemies.transform);
				}
			}
		}
	}
}
