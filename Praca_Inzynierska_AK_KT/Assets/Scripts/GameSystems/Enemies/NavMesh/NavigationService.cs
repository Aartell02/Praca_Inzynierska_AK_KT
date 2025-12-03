using NavMeshPlus.Components;
using UnityEngine;

namespace GameSystems
{
	internal static class NavigationService
	{
		internal static void BakeNavMesh()
		{
			var surface = Object.FindFirstObjectByType<NavMeshSurface>();

			if (surface == null)
			{
				Debug.LogError("NavMeshSurface NOT FOUND! Cannot bake navmesh.");
				return;
			}

			// Nadpisuje stary mesh i generuje nowy
			surface.BuildNavMesh();

			Debug.Log("NavMesh baked after dungeon generation!");
		}
	}
}
