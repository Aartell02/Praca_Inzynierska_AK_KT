using System;
using System.ComponentModel;

namespace GameSystems.Config
{
	[Serializable]
	public struct StatsDto
	{
		public int Health;
		public int Defence;
		public float MovementSpeed;
		public int BasicAttackDamage;
		[DefaultValue(1)]
		public float AttackSpeed;
	}
}
