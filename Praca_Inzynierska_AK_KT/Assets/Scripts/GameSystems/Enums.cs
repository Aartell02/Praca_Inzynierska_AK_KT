using UnityEngine;

namespace GameSystems
{
	public enum AttackType
	{
		QuickAttack,
		NormalAttack,
		HeavyAttack,
	}

	public enum AIEnemyGoal
	{
		Default = int.MinValue,
		None = 0,
		Scout,
		Guard,
		Attack
	}

	public enum PopupType
	{
		PauseMenu,
		LevelUp
	}

	public enum StatType
	{
		Health,
		Defence,
		MovementSpeed,
		AttackDamage,
		AttackSpeed
	}
}
