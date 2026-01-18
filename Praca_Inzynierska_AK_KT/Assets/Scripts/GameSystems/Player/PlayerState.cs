using Core;
using UnityEngine;

namespace GameSystems
{
	class PlayerState : MonoBehaviour
	{
		internal PlayerStats playerStats = PlayerStats.Instance;

		[SerializeField]
		internal float Experience;
		[SerializeField]
		internal int Level = 1;

		internal void Update()
		{
			if(Experience >= 100)
			{
				LevelUp();
				Experience %= 100;
			}
		}

		private void LevelUp()
		{
			Level++;

		}
	}
}
