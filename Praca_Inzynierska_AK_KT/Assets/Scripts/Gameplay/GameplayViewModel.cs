
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

    }
}
