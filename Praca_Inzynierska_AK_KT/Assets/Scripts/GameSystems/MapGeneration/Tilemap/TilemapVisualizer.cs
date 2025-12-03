using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameSystems.MapGeneration
{
    public class TilemapVisualizer : MonoBehaviour
    {
		[SerializeField]
		private Tilemap floorTilemap, wallTilemap;

		[SerializeField]
		private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
			wallInnerCornerDownLeft, wallInnerCornerDownRight,
			wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

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
		}

		internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
		{
			int typeASint = Convert.ToInt32(binaryType, 2);
			TileBase tile = null;

			if(WallTypesHelper.wallInnerCornerDownLeft.Contains(typeASint))
			{
				tile = wallInnerCornerDownLeft;
			}
			else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeASint))
			{
				tile = wallInnerCornerDownRight;
			}
			else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeASint))
			{
				tile = wallDiagonalCornerDownLeft;
			}
			else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeASint))
			{
				tile = wallDiagonalCornerDownRight;
			}
			else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeASint))
			{
				tile = wallDiagonalCornerUpLeft;
			}
			else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeASint))
			{
				tile = wallDiagonalCornerUpRight;
			}
			else if (WallTypesHelper.wallFullEightDirections.Contains(typeASint))
			{
				tile = wallFull;
			}
			else if (WallTypesHelper.wallBottmEightDirections.Contains(typeASint))
			{
				tile = wallBottom;
			}

			if (tile != null) PaintSingleTile(wallTilemap, tile, position);
		}
	}
}
