using Core;
using Core.Inspector;
using System;
using System.ComponentModel;
using UnityEngine;

namespace GameSystems.Config
{
	[CreateAssetMenu(fileName = "StatsConfig", menuName = "Config/StatsConfig")]
	public class StatsConfig : ScriptableObject
	{
		[Header("Player Stats")]
		[SerializeField]
		public StatsDto PlayerStatsData;

		[Header("Enemy Stats")]
		[SerializeField]
		[EnumArray(typeof(EnemyType))]
		public StatsDto[] EnemyStatsData;
	}
}
