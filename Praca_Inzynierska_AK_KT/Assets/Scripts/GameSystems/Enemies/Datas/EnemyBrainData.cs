using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.AI
{
	public class EnemyBrainData : MonoBehaviour
	{
		[SerializeField]
		public AIEnemyOrder Order {  get; private set; }
		[SerializeField]
		public List<Vector2> Altars { get; private set; }
		[SerializeField]
		public Vector2 PlayerPosition { get; private set; }

		public void GiveOrder(AIEnemyOrder order)
		{
			if(Order != order)
				Order = order;
		}

		public bool AddAltarPosition(Vector2 altarPosition)
		{
			if (Altars.Contains(altarPosition))
				return false;

			Altars.Add(altarPosition);
			return true;
		}
		public void AddPlayerPosition(Vector2 playerPosition) => PlayerPosition = playerPosition;

		private void Start()
		{
			Altars = new();
			Order = AIEnemyOrder.None;
		}

	}
}
