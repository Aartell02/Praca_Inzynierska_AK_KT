using UnityEngine;

namespace Gameplay
{
	public class ConfigReferences : MonoBehaviour
	{
		[SerializeField]
		public EnemySpawnConfig enemyConfig;

		[SerializeField]
		public WorldGenerationConfig worldConfig;

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
