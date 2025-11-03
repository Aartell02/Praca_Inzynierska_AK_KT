using DOTS;
using Mono.Cecil.Cil;
using Unity.Scenes;
using UnityEngine;

namespace Gameplay
{
    public static class GameplayViewModel
    {
		private static readonly EnemySpawnConfig enemyConfig = ConfigReferences.Instance.enemyConfig;
		public static void GenerateMap()
		{
			var generator = DungeonGenerator.FindFirstObjectByType<DungeonGenerator>();
			generator.GenerateDungeon();
		}

		public static void SpawnEnemies()
		{
			foreach (var enemyData in enemyConfig.EnemySpawnData)
			{
				DOTSViewModel.SpawnEnemy(enemyData.Type, enemyData.Count);
			}
		}
    }
}
