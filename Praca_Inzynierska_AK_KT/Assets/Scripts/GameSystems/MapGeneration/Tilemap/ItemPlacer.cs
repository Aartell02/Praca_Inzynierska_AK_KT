using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameSystems.MapGeneration
{
	public static class ItemPlacer
	{
		public static void PlaceItems(
			HashSet<Vector2Int> floorPositions,
			HashSet<Vector2Int> wallPositions,
			HashSet<Vector2Int> doorPositions,
			Tilemap walkableItemsTilemap,
			Tilemap unwalkableItemsTilemap,
			List<TileBase> walkableTiles,
			List<TileBase> blockingTiles,
			int minDistanceBetweenItems = 3,
			int wallClearance = 1,
			int doorClearance = 2,
			float spawnChance = 1f,
			float walkableChance = 0.3f
		)
		{
			List<Vector2Int> placedItems = new List<Vector2Int>();

			var shuffledFloors = floorPositions.OrderBy(_ => Random.value).ToList();

			foreach (var pos in shuffledFloors)
			{
				if (Random.value > spawnChance)
					continue;

				if (!IsPositionValid(
					pos,
					floorPositions,
					wallPositions,
					doorPositions,
					placedItems,
					minDistanceBetweenItems,
					wallClearance,
					doorClearance))
					continue;

				bool spawnWalkable = Random.value < walkableChance;

				Tilemap targetTilemap = spawnWalkable ? walkableItemsTilemap : unwalkableItemsTilemap;
				var sourceList = spawnWalkable ? walkableTiles : blockingTiles;

				if (sourceList == null || sourceList.Count == 0)
					continue;

				TileBase selectedItem = sourceList[Random.Range(0, sourceList.Count)];

				Vector3Int cellPos = targetTilemap.WorldToCell((Vector3Int)pos);
				targetTilemap.SetTile(cellPos, selectedItem);

				placedItems.Add(pos);
			}

			Debug.Log($"[ItemPlacer] Items placed: {placedItems.Count}");
		}

		public static bool IsPositionValid(
			Vector2Int pos,
			HashSet<Vector2Int> floorPositions,
			HashSet<Vector2Int> wallPositions,
			HashSet<Vector2Int> doorPositions,
			List<Vector2Int> placedItems,
			int minItemDistance,
			int wallClearance,
			int doorClearance)
		{
			foreach (var dir in Direction2D.eightDirectionList)
			{
				for (int i = 1; i <= wallClearance; i++)
				{
					if (wallPositions.Contains(pos + dir * i))
						return false;
				}
			}

			foreach (var door in doorPositions)
			{
				if (Vector2Int.Distance(pos, door) <= doorClearance)
					return false;
			}

			foreach (var item in placedItems)
			{
				if (Vector2Int.Distance(pos, item) < minItemDistance)
					return false;
			}

			int floorNeighbours = 0;
			foreach (var dir in Direction2D.cardinalDirectionsList)
			{
				if (floorPositions.Contains(pos + dir))
					floorNeighbours++;
			}

			return floorNeighbours > 1;
		}
	}
}
