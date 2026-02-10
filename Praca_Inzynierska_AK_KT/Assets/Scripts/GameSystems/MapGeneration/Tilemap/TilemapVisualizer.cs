using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

namespace GameSystems.MapGeneration
{
	public class TilemapVisualizer : MonoBehaviour
	{
		[SerializeField]
		private Tilemap floorTilemap, wallTilemap, backgroundTilemap;

		[SerializeField]
		private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
			wallInnerCornerDownLeft, wallInnerCornerDownRight,
			wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft, backgroundTile;

		public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
		{
			PaintTiles(floorPositions, floorTilemap, floorTile);
		}

		private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
		{
			foreach (var position in positions)
			{
				PaintSingleTile(tilemap, tile, position);
			}
		}

		public void PaintBackground(IEnumerable<Vector2Int> floorPositions, IEnumerable<Vector2Int> wallPositions)
		{
			HashSet<Vector2Int> occupiedPositions = new HashSet<Vector2Int>();
			occupiedPositions.UnionWith(floorPositions);
			occupiedPositions.UnionWith(wallPositions);

			int minX = int.MaxValue, maxX = int.MinValue;
			int minY = int.MaxValue, maxY = int.MinValue;

			foreach (var pos in occupiedPositions)
			{
				if (pos.x < minX) minX = pos.x;
				if (pos.x > maxX) maxX = pos.x;
				if (pos.y < minY) minY = pos.y;
				if (pos.y > maxY) maxY = pos.y;
			}

			int padding = 10;

			for (int x = minX - padding; x <= maxX + padding; x++)
			{
				for (int y = minY - padding; y <= maxY + padding; y++)
				{
					Vector2Int position = new Vector2Int(x, y);

					// Opcja A: Jeśli tło ma być tylko tam, gdzie jest pustka:
					if (!occupiedPositions.Contains(position))
					{
						PaintSingleTile(backgroundTilemap, backgroundTile, position);
					}

					// Opcja B: Jeśli tło jest na warstwie pod spodem (Sorting Layer -1),
					// możesz usunąć 'if' i malować wszystko. To zapobiega powstawaniu
					// "szpar" między ścianami a tłem przy przesuwaniu kamery.
				}
			}
		}

		internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
		{
			int typeAsInt = Convert.ToInt32(binaryType, 2);
			TileBase tile = null;
			if (WallTypesHelper.wallTop.Contains(typeAsInt))
			{
				tile = wallTop;
			}
			else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
			{
				tile = wallSideRight;
			}
			else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
			{
				tile = wallSideLeft;
			}
			else if (WallTypesHelper.wallBottom.Contains(typeAsInt))
			{
				tile = wallBottom;
			}
			else if (WallTypesHelper.wallFull.Contains(typeAsInt))
			{
				tile = wallFull;
			}
			if (tile != null) PaintSingleTile(wallTilemap, tile, position);
		}

		private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
		{
			var tilePosition = tilemap.WorldToCell((Vector3Int)position);
			tilemap.SetTile(tilePosition, tile);
		}

		public void ClearTiles()
		{
			floorTilemap.ClearAllTiles();
			wallTilemap.ClearAllTiles();
			backgroundTilemap.ClearAllTiles();
		}

		internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
		{
			int typeASint = Convert.ToInt32(binaryType, 2);
			TileBase tile = null;

			if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeASint)) tile = wallInnerCornerDownLeft;
			else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeASint)) tile = wallInnerCornerDownRight;
			else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeASint)) tile = wallDiagonalCornerDownLeft;
			else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeASint)) tile = wallDiagonalCornerDownRight;
			else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeASint)) tile = wallDiagonalCornerUpLeft;
			else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeASint)) tile = wallDiagonalCornerUpRight;
			else if (WallTypesHelper.wallFullEightDirections.Contains(typeASint)) tile = wallFull;
			else if (WallTypesHelper.wallBottmEightDirections.Contains(typeASint)) tile = wallBottom;

			if (tile != null) PaintSingleTile(wallTilemap, tile, position);
		}
	}
}
