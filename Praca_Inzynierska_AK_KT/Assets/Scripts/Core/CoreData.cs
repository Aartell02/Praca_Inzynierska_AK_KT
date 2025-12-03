using UnityEngine;

namespace Core
{
	public static class CoreData
	{
		static Vector2 EnemySpawnPoint;
		static Vector2 PlayerSpawnPoint;

		public static void SetSpawnPoints(Vector2 playerSpawnPoint, Vector2 enemySpawnPoint)
		{
			PlayerSpawnPoint = playerSpawnPoint;
			EnemySpawnPoint = enemySpawnPoint;
		}

		public static Vector2 GetPlayerSpawnPoint() => PlayerSpawnPoint;
		public static Vector2 GetEnemySpawnPoint() => EnemySpawnPoint;
	}
}
