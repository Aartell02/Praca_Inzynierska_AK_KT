using Core;
using Core.Inspector;
using System;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems.Config
{
	[CreateAssetMenu(fileName = "StatsConfig", menuName = "Config/StatsConfig")]
	public class StatsConfig : ScriptableObject
	{
		[Header("Player Stats")]
		public StatsDto PlayerStatsData;

		[Header("Stat Upgrade Ranges")]
		[EnumArray(typeof(StatType))]
		public float2[] StatsUpgradeRanges;

		[Header("Enemy Stats")]
		[EnumArray(typeof(EnemyType))]
		public StatsDto[] EnemyStatsData;
	}
}
