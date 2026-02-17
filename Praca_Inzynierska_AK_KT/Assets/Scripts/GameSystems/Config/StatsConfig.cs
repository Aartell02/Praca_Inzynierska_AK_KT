using Core;
using Core.Dto;
#if UNITY_EDITOR
using Editor;
#endif
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
#if UNITY_EDITOR
		[EnumArray(typeof(StatType))]
#endif
		public float2[] StatsUpgradeRanges;

		[Header("Enemy Stats")]
#if UNITY_EDITOR
		[EnumArray(typeof(EnemyType))]
#endif
		public StatsDto[] EnemyStatsData;
	}
}
