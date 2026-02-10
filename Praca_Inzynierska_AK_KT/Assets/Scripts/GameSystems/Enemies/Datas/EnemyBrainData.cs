using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystems
{
	public class EnemyBrainData : MonoBehaviour
	{
		public bool dirty = false;
		public AIEnemyGoal Goal { get; private set; }
		public AIEnemyGoal Order { get; private set; }
		public List<AltarData> Altars { get; private set; }
		public Vector2 PlayerPosition { get; private set;}
		public Vector2 DefaultPosition { get; private set; }
		public int InfoPoints { get; private set; }
		public bool HasPosition { get; private set; }

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

		public bool AddAltarPosition(AltarData altar)
		{
			if (Altars.Contains(altar))
				return false;

			Altars.Add(altar);
			InfoPoints++;
			return true;
		}
		public void ClearInfo() => InfoPoints = 0;

		public void AddPlayerPosition(Vector2 playerPosition) => PlayerPosition = playerPosition;

		public void SetDeufaultPosition(Vector2 deufaultPosition)
		{
			HasPosition = true;
			DefaultPosition = deufaultPosition;
		}

		private void Awake()
		{
			Altars = new();
			HasPosition = false;
		}

		private void Start()
		{
			DefaultPosition = EnemySharedData.SpawnPoint;
			InfoPoints = 0;
		}
	}
}
