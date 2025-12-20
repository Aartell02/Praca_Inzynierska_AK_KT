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
		public EnemyCommunicationData EnemyCommunicationData;
		public EnemyAttackData EnemyAttackData;
	}

	[Serializable]
	public class EnemyCommunicationData
	{
		public float CommunicationRadius = 5f;
		public float SensorRadius = 10f;
	}

	[Serializable]
	public class EnemyAttackData
	{
		public float MeleeAttackRadius = 1.5f;
		public float MeleeAttackDelay = 1;
	}
}
