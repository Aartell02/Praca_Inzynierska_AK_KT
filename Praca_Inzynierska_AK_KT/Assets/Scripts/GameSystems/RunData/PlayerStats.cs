using Core;
using GameSystems.Config;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
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

		public void SetStats(StatsDto stats)
		{
			Health = stats.Health;
			Defence = stats.Defence;
			MovementSpeed = stats.MovementSpeed;
			BasicAttackDamage = stats.BasicAttackDamage;
			AttackSpeed = stats.AttackSpeed;
		}
		public float GetStat(StatType stat)
		{
			switch (stat)
			{
				case StatType.Health:
					return Health;
				case StatType.Defence:
					return Defence;
				case StatType.MovementSpeed:
					return MovementSpeed;
				case StatType.AttackSpeed:
					return AttackSpeed;
				case StatType.AttackDamage:
					return BasicAttackDamage;
			}
			return new float();
		}
		public void UpgradeStat(StatType stat, float value)
		{
			switch (stat)
			{
				case StatType.Health: Health += (int)value; break;
				case StatType.Defence: Defence += (int)value; break;
				case StatType.MovementSpeed: MovementSpeed += value; break;
				case StatType.AttackSpeed: AttackSpeed += value; break;
				case StatType.AttackDamage: BasicAttackDamage += (int)value; break;
			}
		}
	}
}
