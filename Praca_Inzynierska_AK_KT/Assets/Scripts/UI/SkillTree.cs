using Core.Services;
using Gameplay;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameSystems
{
	class SkillTree : MonoBehaviour
	{
		[SerializeField]
		private GameObject Body;

		[SerializeField]
		private SkillTreeOption[] Options;

		bool currentlyLeveling = false;

		private void Start()
		{
			Body.SetActive(false);
		}

		private void Update()
		{
			if(PlayerStats.Instance.SkillPoints > 0 && !currentlyLeveling)
			{
				currentlyLeveling = true;
				PlayerStats.Instance.SkillPoints--;
				LevelUP();
			}
		}

		public void LevelUP()
		{
			GameRunState.PauseGame();
			Body.SetActive(true);
			List<StatType> statups = new();
			foreach (var item in Options)
				for (int attempts = 10; attempts > 0; attempts--)
				{
					var stat = (StatType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(StatType)).Length);
					if (!statups.Contains(stat))
					{
						statups.Add(stat);

						item.Initialize(this, stat);
						break;
					}
				}
		}
		public void ResumeGame()
		{
			GameRunState.ResumeGame();
			Body.SetActive(false);
			SelectReward(Options[0].Stat, 10);
		}

		public void SelectReward(StatType stat, float value)
		{
			PlayerStats.Instance.UpgradeStat(stat, value);
			Body.SetActive(false);
			GameRunState.ResumeGame();
			currentlyLeveling = false;
		}
	}
}
