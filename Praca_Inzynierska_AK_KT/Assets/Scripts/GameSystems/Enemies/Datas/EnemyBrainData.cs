using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.AI
{
	public class EnemyBrainData : MonoBehaviour
	{
		[SerializeField]
		public AIEnemyOrder Order {  get; private set; }
		[SerializeField]
		public List<Transform> Altars { get; private set; }
		[SerializeField]
		public Vector2 PlayerPosition { get; private set; }

		public void GiveOrder(AIEnemyOrder order)
		{
			if(Order != order)
				Order = order;
		}

		public bool AddAltarPosition(Transform altarTransform)
		{
			if (Altars.Contains(altarTransform))
				return false;

			Altars.Add(altarTransform);
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
