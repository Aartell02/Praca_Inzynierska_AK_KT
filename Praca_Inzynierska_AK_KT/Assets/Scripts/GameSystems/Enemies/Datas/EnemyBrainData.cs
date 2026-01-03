using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.AI
{
	public class EnemyBrainData : MonoBehaviour
	{
		[SerializeField]
		public AIEnemyOrder Order {  get; private set; }
		[SerializeField]
		public List<GameObject> Altars { get; set; }
		[SerializeField]
		public Vector2 PlayerPosition { get; private set; }
		[SerializeField]
		public Vector2 DefaultPosition { get; private set; }

		public void GiveOrder(AIEnemyOrder order)
		{
			if(Order != order)
				Order = order;
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
			Order = AIEnemyOrder.None;
		}

	}
}
