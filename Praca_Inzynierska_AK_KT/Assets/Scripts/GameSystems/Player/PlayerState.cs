using Core;
using NUnit.Framework.Interfaces;
using UnityEngine;

namespace GameSystems
{
	class PlayerState : MonoBehaviour
	{
		internal PlayerStats playerStats = PlayerStats.Instance;

		[SerializeField]
		internal float Experience;
		[SerializeField]
		private int Level = 1;

		internal void Start()
		{
			Experience = playerStats.Experience;
			Level = playerStats.Level;
		}

		internal void Update()
		{
			playerStats.Experience = Experience;
			playerStats.Level = Level;
			if (Experience >= 100)
			{
				Level++;
				playerStats.SkillPoints++;
				Experience %= 100;
			}
		}
	}
}
