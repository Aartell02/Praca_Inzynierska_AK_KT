using System.Collections.Generic;
using UnityEngine;

namespace GameSystems
{
	public class EnemyBrainData : MonoBehaviour
	{
		public bool dirty = false;
		public AIEnemyGoal Goal { get; private set; }
		public AIEnemyGoal Order { get; private set; }
		public List<GameObject> Altars { get; private set; }
		public Vector2 PlayerPosition { get; private set;}
		public Vector2 DefaultPosition { get; private set; }

		public void SetGoal(AIEnemyGoal goal, bool isOrder = false)
		{
			if (isOrder)
				Order = goal;
			if (Goal != goal)
			{
				Goal = goal;
				dirty = true;
			}
		}

		public bool AddAltarPosition(GameObject altarTransform)
		{
			if (Altars.Contains(altarTransform))
				return false;

			Altars.Add(altarTransform);
			return true;
		}

		public void AddPlayerPosition(Vector2 playerPosition) => PlayerPosition = playerPosition;

		public void SetDeufaultPosition(Vector2 deufaultPosition) => PlayerPosition = deufaultPosition;

		private void Start()
		{
			Altars = new();
		}
	}
}
