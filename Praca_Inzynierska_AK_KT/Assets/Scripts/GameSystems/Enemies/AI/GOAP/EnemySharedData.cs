using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.AI
{
	public static class EnemySharedData
	{
		public static List<Transform> Commanders = new();
		public static Vector2 SpawnPoint;
	}
}
