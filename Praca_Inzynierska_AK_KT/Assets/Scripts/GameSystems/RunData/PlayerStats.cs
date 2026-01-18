using Core;
using GameSystems.Config;
using UnityEngine;

namespace GameSystems
{
	public class PlayerStats : MonoBehaviour
	{
		public static PlayerStats Instance { get; private set; }

		StatsConfig statsConfig = ConfigReferences.Instance.statsConfig;
		public int Health;
		public int Defence;
		public float MovementSpeed;
		public int BasicAttackDamage;
		public float AttackSpeed;

		public float Experience;

		public int Level = 1;

		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}
			Instance = this;
		}

		internal void Start()
		{
			Health = statsConfig.PlayerStatsData.Health;
			Defence = statsConfig.PlayerStatsData.Defence;
			MovementSpeed = statsConfig.PlayerStatsData.MovementSpeed;
			BasicAttackDamage = statsConfig.PlayerStatsData.BasicAttackDamage;
			AttackSpeed = statsConfig.PlayerStatsData.AttackSpeed;
		}

		internal void Update()
		{
			if (Experience >= 100)
			{
				LevelUp();
				Experience %= 100;
			}
		}

		private void LevelUp()
		{
			Level++;

		}

		void SetStats(StatsDto stats)
		{

		}
	}
}
