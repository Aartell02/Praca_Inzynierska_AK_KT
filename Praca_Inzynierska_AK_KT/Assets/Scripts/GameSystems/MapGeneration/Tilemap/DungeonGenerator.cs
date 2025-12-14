using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;
using Core;
using GameSystems.Config;
using GameSystems.Data;

namespace GameSystems.MapGeneration
{
    public class DungeonGenerator : MonoBehaviour
	{
		[SerializeField]
		private TilemapVisualizer tilemapVisualizer;

		[SerializeField]
		private Tilemap doorsTilemap;

		[SerializeField]
		private TileBase doorLeftTile;

		[SerializeField]
		private TileBase doorRightTile;

		[SerializeField]
		private Tilemap itemsTilemap;

		[SerializeField]
		private TileBase bones, box1, box2, box3, box4, rocks, table1, table2, torch1, torch2;

		private WorldGenerationConfig worldConfig = ConfigReferences.Instance.worldConfig;

		private HashSet<Vector2Int> doorPositions = new HashSet<Vector2Int>();

		private Vector2Int startPosition;
		private int corridorLength;
		private int corridorCount;
		private float roomPercent;
		private RandomWalkData randomWalkParameters;

		public DungeonGenerator()
		{
			startPosition = worldConfig.startPosition;
			corridorLength = worldConfig.corridorLength;
			corridorCount = worldConfig.corridorCount;
			roomPercent = worldConfig.roomPercent;
			randomWalkParameters = worldConfig.randomWalkParameters;
		}
		public void GenerateDungeon()
		{
			tilemapVisualizer.ClearTiles();
			RunProceduralGeneration();
		}
		protected void RunProceduralGeneration()
		{
			CorridorFirstGeneration();
		}
		protected HashSet<Vector2Int> RunRandomWalk(RandomWalkData parameters, Vector2Int position)
		{
			var currentPosition = position;
			HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

			for (int i = 0; i < randomWalkParameters.iterations; i++)
			{
				var path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);
				floorPositions.UnionWith(path);
				if (randomWalkParameters.startRandomlyEachIteration)
				{
					currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
				}
			}
			return floorPositions;
		}
		private void CorridorFirstGeneration()
		{
			HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
			HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

			List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

			HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

			List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

			CreateRoomsAtDeadEnd(deadEnds, roomPositions);

			floorPositions.UnionWith(roomPositions);

			for (int i = 0; i < corridors.Count; i++)
			{
				corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
				floorPositions.UnionWith(corridors[i]);
			}

			tilemapVisualizer.PaintFloorTiles(floorPositions);
			WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

			PlaceDoors(floorPositions);

			HashSet<Vector2Int> wallPositions = GetWallPositions(floorPositions);

			ItemPlacer.PlaceItems(
				floorPositions,
				wallPositions,
				doorPositions,
				itemsTilemap,
				new List<TileBase> { bones, box1, box2, box3, box4, rocks, table1, table2, torch1, torch2 },
				minDistanceBetweenItems: 3,
				wallClearance: 1,
				doorClearance: 2,
				spawnChance: 0.2f
			);

		}

		public List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
		{
			List<Vector2Int> newCorridor = new List<Vector2Int>();
			Vector2Int previewDirection = Vector2Int.zero;
			for (int i=1; i<corridor.Count; i++)
			{
				Vector2Int directionFromCell = corridor[i] - corridor[i - 1];
				if(previewDirection != Vector2Int.zero && directionFromCell != previewDirection)
				{
					for (int x=-1;x<2;x++)
					{
						for(int y = -1; y < 2; y++)
						{
							newCorridor.Add(corridor[i-1]+new Vector2Int(x,y));
						}
						previewDirection = directionFromCell;
					}
				}
				else
				{
					Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
					newCorridor.Add(corridor[i - 1]);
					newCorridor.Add(corridor[i-1] + newCorridorTileOffset);
				}
			}
			return newCorridor;
		}

		private Vector2Int GetDirection90From(Vector2Int direction)
		{
			if (direction == Vector2Int.up) return Vector2Int.right;
			if (direction == Vector2Int.right) return Vector2Int.down;
			if (direction == Vector2Int.down) return Vector2Int.left;
			if (direction == Vector2Int.left) return Vector2Int.up;
			return Vector2Int.zero;
		}

		private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
		{
			foreach (var position in deadEnds)
			{
				if(roomFloors.Contains(position) == false)
				{
					var roomFloor = RunRandomWalk(randomWalkParameters, position);
					roomFloors.UnionWith(roomFloor);
				}
			}
		}

		private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
		{
			List<Vector2Int> deadEnds = new List<Vector2Int>();
			foreach (var position in floorPositions)
			{
				int neighboursCount = 0;
				foreach (var direction in Direction2D.cardinalDirectionsList)
				{
					if (floorPositions.Contains(position + direction))
					{
						neighboursCount++;
					}
				}
				if (neighboursCount == 1)
				{
					deadEnds.Add(position);
				}
			}

			return deadEnds;
		}

		private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
		{
			HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
			int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count*roomPercent);

			List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

			foreach (var roomPosition in roomToCreate)
			{
				var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
				roomPositions.UnionWith(roomFloor);
			}
			return roomPositions;
		}

		private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
		{
			var currentPosition = startPosition;
			potentialRoomPositions.Add(currentPosition);
			List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

			for (int i = 0; i < corridorCount; i++)
			{
				var corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(currentPosition, corridorLength);
				corridors.Add(corridor);
				currentPosition = corridor[corridor.Count - 1];
				potentialRoomPositions.Add(currentPosition);
				floorPositions.UnionWith(corridor);
			}
			return corridors;
		}

		private void PlaceDoors(HashSet<Vector2Int> floorPositions)
		{
			// 1. Wyznacz ściany wokół podłogi
			HashSet<Vector2Int> wallPositions = GetWallPositions(floorPositions);

			// 2. Dolne drzwi
			var bottom = FindDoorWallSpot(floorPositions, wallPositions, findTop: false);
			doorsTilemap.SetTile((Vector3Int)bottom[0], doorLeftTile);
			doorsTilemap.SetTile((Vector3Int)bottom[1], doorRightTile);

			// 3. Górne drzwi
			var top = FindDoorWallSpot(floorPositions, wallPositions, findTop: true);
			doorsTilemap.SetTile((Vector3Int)top[0], doorLeftTile);
			doorsTilemap.SetTile((Vector3Int)top[1], doorRightTile);

			CoreData.SetSpawnPoints(new Vector2(bottom[0].x + 0.5f, bottom[0].y + 2f), new Vector2(top[0].x + 0.5f, top[0].y - 1f));

			doorPositions.Clear();

			doorPositions.Add(bottom[0]);
			doorPositions.Add(bottom[1]);
			doorPositions.Add(top[0]);
			doorPositions.Add(top[1]);

			Debug.Log($"Drzwi dol: {bottom[0]} , {bottom[1]}");
			Debug.Log($"Drzwi gora: {top[0]} , {top[1]}");
		}

		private Vector2Int[] FindDoorWallSpot(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> wallPositions, bool findTop)
		{
			var rows = wallPositions.Select(p => p.y).Distinct().ToList();
			rows.Sort();
			if (findTop) rows.Reverse();

			foreach (var y in rows)
			{
				var row = wallPositions.Where(p => p.y == y).OrderBy(p => p.x).ToList();

				for (int i = 0; i < row.Count - 1; i++)
				{
					Vector2Int a = row[i];
					Vector2Int b = row[i + 1];

					if (b.x != a.x + 1) continue;

					bool aClear = !wallPositions.Contains(a + Vector2Int.up) && !wallPositions.Contains(a + Vector2Int.down);
					bool bClear = !wallPositions.Contains(b + Vector2Int.up) && !wallPositions.Contains(b + Vector2Int.down);

					if (aClear && bClear)
						return new[] { a, b };
				}
			}

			var fallbackRow = wallPositions.Where(p => p.y == (findTop ? rows[0] : rows.Last())).OrderBy(p => p.x).ToList();
			return new[] { fallbackRow[0], fallbackRow[Math.Min(1, fallbackRow.Count - 1)] };
		}

		private HashSet<Vector2Int> GetWallPositions(HashSet<Vector2Int> floorPositions)
		{
			HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
			List<Vector2Int> directions = Direction2D.eightDirectionList;

			foreach (var pos in floorPositions)
			{
				foreach (var dir in directions)
				{
					var neighbour = pos + dir;
					if (!floorPositions.Contains(neighbour))
					{
						wallPositions.Add(neighbour);
					}
				}
			}
			return wallPositions;
		}
	}
}
