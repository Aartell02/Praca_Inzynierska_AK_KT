using GameSystems.AI;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems
{
	class ScoutData : EnemyData
	{
		public int ScoutsCount { get; private set; }

		public List<Vector2> Altars { get; private set; }


		internal void AddAltarPosition(Vector2 altarPosition) => Altars.Add(altarPosition);


	}
}
