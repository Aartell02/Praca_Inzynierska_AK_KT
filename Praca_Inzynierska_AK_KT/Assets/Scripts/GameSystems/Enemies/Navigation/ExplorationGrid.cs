// ExplorationGrid.cs - Struktura danych
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameSystems
{
	/*
	public class ExplorationGrid : MonoBehaviour
	{
		private Tilemap tilemap;

		// Tablica bitowa dla oszczędności pamięci (0=Unknown, 1=Visited)
		// Mapowanie: [x - bounds.xMin, y - bounds.yMin]
		private System.Collections.BitArray visitedMap;
		private BoundsInt bounds;

		public void Initialize()
		{
			tilemap.CompressBounds();
			bounds = tilemap.cellBounds;
			int size = bounds.size.x * bounds.size.y;
			visitedMap = new System.Collections.BitArray(size, false);
		}

		public bool IsVisited(Vector3 worldPos)
		{
			Vector3Int cell = tilemap.WorldToCell(worldPos);
			int index = GetIndex(cell);
			if (index == -1) return true; // Poza mapą traktujemy jako odwiedzone
			return visitedMap[index];
		}

		public void MarkVisited(Vector3 worldPos, int radius)
		{
			Vector3Int center = tilemap.WorldToCell(worldPos);
			// Pętla po promieniu w przestrzeni siatki
			for (int x = -radius; x <= radius; x++)
			{
				for (int y = -radius; y <= radius; y++)
				{
					// Sprawdzenie dystansu euklidesowego na siatce
					if (x * x + y * y <= radius * radius)
					{
						SetVisited(center + new Vector3Int(x, y, 0));
					}
				}
			}
		}

		// Metoda pomocnicza dla algorytmu Frontier
		public List<Vector3Int> GetNeighbors(Vector3Int cell)
		{
			// Zwraca 4 lub 8 sąsiadów
		}
	}
	*/
}
