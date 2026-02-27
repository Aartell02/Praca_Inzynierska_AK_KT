#if UNITY_EDITOR
using Editor;
#endif
using TMPro;
using UnityEngine;

namespace GameSystems
{
	class StatView : MonoBehaviour
	{
		PlayerStats playerStats = PlayerStats.Instance;
		[SerializeField]
#if UNITY_EDITOR
		[EnumArray(typeof(StatType))]
#endif
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
