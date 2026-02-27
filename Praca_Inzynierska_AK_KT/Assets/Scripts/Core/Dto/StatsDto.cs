using System;
using System.ComponentModel;

namespace Core.Dto
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
