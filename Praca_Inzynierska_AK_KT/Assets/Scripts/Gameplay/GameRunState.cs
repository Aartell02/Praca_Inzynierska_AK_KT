using Boot;
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

		public static int LoadNextFloor()
		{
			CurrentFloor++;
			if (CurrentFloor > FloorCount)
			{
				BootViewModel.FinishGame(true);
				return CurrentFloor;
			}
			Debug.Log($"Piętro: {CurrentFloor}");
			BootViewModel.LoadFloor();
			return CurrentFloor;
		}
		public static void SetFloorCount(int floorCount) => FloorCount = floorCount;
	}
}
