using UnityEngine;

namespace GameSystems.Config
{
	public class ConfigReferences : MonoBehaviour
	{
		[SerializeField]
		public EnemySpawnConfig enemyConfig;

		[SerializeField]
		public WorldGenerationConfig worldConfig;

		[SerializeField]
		public RunConfig runConfig;

		[SerializeField]
		public StatsConfig statsConfig;

		public static ConfigReferences Instance { get; private set; }

		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}
			Instance = this;
		}
	}
}
