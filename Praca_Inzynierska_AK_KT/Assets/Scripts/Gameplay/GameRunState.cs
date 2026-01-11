
using Core;
using GameSystems;
using GameSystems.MapGeneration;
using UnityEngine;

namespace Gameplay
{
	public static class GameRunState
	{
		public static int FloorCount { get; private set; }
		public static int CurrentFloor { get; private set; }

		public static void NextFloor() => CurrentFloor++;
		public static void SetFloorCount(int floorCount) => FloorCount = floorCount;
	}
}
