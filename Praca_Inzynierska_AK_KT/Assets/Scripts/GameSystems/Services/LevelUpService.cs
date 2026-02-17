using GameSystems;
using GameSystems.Config;
using UnityEngine;

public static class LevelUpService
{
	public static float GenerateValue(StatType stat)
	{
		StatsConfig _config = ConfigReferences.Instance.statsConfig;
		var value = _config.StatsUpgradeRanges[(int)stat];
		return Random.Range(value.x,value.y);
	}
}
