using Core.Inspector;
using Core.Services;
using Gameplay;
using GameSystems.Config;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSystems
{
	class StatView : MonoBehaviour
	{
		PlayerStats playerStats = PlayerStats.Instance;
		[SerializeField]
		[EnumArray(typeof(StatType))]
		private TextMeshProUGUI[] _stats;

		public void Update()
		{
			for( int i = 0; i < _stats.Length; i++)
			{
				var stat = _stats[i];

				stat.text = $"{(StatType)i}: {playerStats.GetStat((StatType)i).ToString("F2")}";
			}
		}
	}
}
