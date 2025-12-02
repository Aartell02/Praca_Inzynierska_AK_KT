
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay
{
    public static class GameplayViewModel
    {
		private static readonly EnemySpawnConfig enemyConfig = ConfigReferences.Instance.enemyConfig;

		private static Vector2 PlayerSpawnPoint;
		private static Vector2 EnemySpawnPoint;

		public static void GenerateMap()
		{
			var generator = DungeonGenerator.FindFirstObjectByType<DungeonGenerator>();
			generator.GenerateDungeon();
		}

		public static void SetSpawnPoints(Vector2 playerSpawnPoint, Vector2 enemySpawnPoint)
		{
			PlayerSpawnPoint = playerSpawnPoint;
			EnemySpawnPoint = enemySpawnPoint;
		}

		public static Vector2 GetPlayerSpawnPoint() => PlayerSpawnPoint;
		public static Vector2 GetEnemySpawnPoint() => EnemySpawnPoint;

    }
}
