using Core;
using Core.Inspector;
using System;
using UnityEngine;

namespace GameSystems.AI
{
	[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Config/GameSystems/EnemyConfig")]
	public class EnemyConfig : ScriptableObject
	{
		[Header("Enemy stats configuration")]
		[SerializeField]
		[EnumArray(typeof(EnemyType))]
		public EnemyAttackData[] EnemyData;
	}

	[Serializable]
	public class EnemyAttackData
	{
		public EnemyType Type;
		public float SensorRadius = 10f;
		public float MeleeAttackRadius = 1f;
		public int MeleeAttackCost = 1;
		public float MeleeAttackDelay = 1;
		public LayerMask AttackableLayerMask;
	}
}
