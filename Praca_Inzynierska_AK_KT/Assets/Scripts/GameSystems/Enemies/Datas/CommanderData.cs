using GameSystems.AI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystems
{
	class CommanderData : EnemyData
	{
		public int SoldiersCount { get; private set; }
		public int ScoutsCount { get; private set; }

		public List<Vector2> Altars { get; private set; }

		internal void AddSoldier() => SoldiersCount++;

		internal void AddAltarPosition(Vector2 altarPosition) => Altars.Add(altarPosition);

		private void Start()
		{
			var result = EnemySharedData.Commanders[0] = transform;
			Debug.Log(result);
		}
	}
}
