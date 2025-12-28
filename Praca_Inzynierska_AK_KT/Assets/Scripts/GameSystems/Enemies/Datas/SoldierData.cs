using System.Collections.Generic;
using UnityEngine;

namespace GameSystems
{
	class SoldierData : MonoBehaviour
	{
		public int SoldiersCount { get; private set; }
		public int ScoutsCount { get; private set; }

		public List<Vector2> Altars { get; private set; }

		internal void AddSoldier() => SoldiersCount++;

		internal void AddAltarPosition(Vector2 altarPosition) => Altars.Add(altarPosition);

	}
}
