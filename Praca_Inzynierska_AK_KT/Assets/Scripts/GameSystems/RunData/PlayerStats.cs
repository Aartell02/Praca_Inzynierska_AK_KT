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
		public int SkillPoints = 0;

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
			SetStats(statsConfig.PlayerStatsData);
		}

		void SetStats(StatsDto stats)
		{
			Health = stats.Health;
			Defence = stats.Defence;
			MovementSpeed = stats.MovementSpeed;
			BasicAttackDamage = stats.BasicAttackDamage;
			AttackSpeed = stats.AttackSpeed;
		}
		public void UpgradeStat(Stat stat, int value)
		{
			switch (stat)
			{
				case Stat.Health: Health += value; break;
				case Stat.Defence: Defence += value; break;
				case Stat.MovementSpeed: MovementSpeed += value; break;
				case Stat.AttackSpeed: AttackSpeed += value; break;
				case Stat.AttackDamage: BasicAttackDamage += value; break;
			}
		}
	}
}
