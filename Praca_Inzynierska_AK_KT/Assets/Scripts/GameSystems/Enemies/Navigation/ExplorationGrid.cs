using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class ExplorationGrid : MonoBehaviour
{
	private Tilemap floorTilemap;

	// HashSet jest szybki (O(1)), więc idealny do sprawdzania "czy już tu byłem"
	private HashSet<Vector3Int> exploredTiles = new HashSet<Vector3Int>();

	private BoundsInt mapBounds;
	private int totalTilesCount;

	// Singleton
	public static ExplorationGrid Instance { get; private set; }

	private void Awake()
	{
		if (floorTilemap == null)
			floorTilemap = GetComponent<Tilemap>();

		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
	}

	private void Start()
	{
		InitializeGrid();
	}

	public void InitializeGrid()
	{
		floorTilemap.CompressBounds();
		mapBounds = floorTilemap.cellBounds;
		exploredTiles.Clear();

		totalTilesCount = 0;
		foreach (var pos in mapBounds.allPositionsWithin)
		{
			if (floorTilemap.HasTile(pos))
			{
				totalTilesCount++;
			}
		}
	}

	/// <summary>
	/// Odkrywa obszar wokół podanej pozycji.
	/// </summary>
	public void RevealArea(Vector3 worldPosition, int radius)
	{
		Vector3Int centerCell = floorTilemap.WorldToCell(worldPosition);

		// Optymalizacja: Sprawdzamy kwadrat, a w nim koło
		for (int x = -radius; x <= radius; x++)
		{
			for (int y = -radius; y <= radius; y++)
			{
				if (x * x + y * y <= radius * radius)
				{
					Vector3Int targetCell = new Vector3Int(centerCell.x + x, centerCell.y + y, centerCell.z);
					MarkTileAsExplored(targetCell);
				}
			}
		}
	}

	private void MarkTileAsExplored(Vector3Int cellPos)
	{
		if (exploredTiles.Contains(cellPos) || !floorTilemap.HasTile(cellPos))
			return;

		exploredTiles.Add(cellPos);
	}

	public bool IsExplored(Vector3 worldPos)
	{
		Vector3Int cell = floorTilemap.WorldToCell(worldPos);
		if (!floorTilemap.HasTile(cell)) return true; // Poza mapą = "odkryte" (nie idź tam)
		return exploredTiles.Contains(cell);
	}

	public float GetExplorationProgress()
	{
		if (totalTilesCount == 0) return 1f;
		return (float)exploredTiles.Count / totalTilesCount;
	}

	/// <summary>
	/// GŁÓWNA METODA DLA SENSORA
	/// Próbuje znaleźć cel eksploracji.
	/// 1. Najpierw próbuje szybko wylosować coś blisko (Random).
	/// 2. Jeśli się nie uda (agent jest w bazie), szuka NAJBLIŻSZEGO nieodkrytego kafelka metodą BFS.
	/// </summary>
	public bool TryGetUnexploredTarget(Vector3 center, float randomRange, out Vector3 result)
	{
		// KROK 1: Szybkie losowanie (tanie obliczeniowo)
		// Dobre, gdy jesteśmy na froncie i mamy dużo nieodkrytego terenu wokół
		int attempts = 15;
		for (int i = 0; i < attempts; i++)
		{
			Vector2 randomOffset = Random.insideUnitCircle * randomRange;
			Vector3 targetPos = center + new Vector3(randomOffset.x, randomOffset.y, 0);
			Vector3Int cell = floorTilemap.WorldToCell(targetPos);

			if (floorTilemap.HasTile(cell) && !exploredTiles.Contains(cell))
			{
				result = floorTilemap.GetCellCenterWorld(cell);
				return true;
			}
		}

		// KROK 2: Fallback - BFS (Przeszukiwanie wszerz)
		// Uruchamiane tylko, gdy losowanie zawiedzie (np. agent wrócił do bazy)
		// Znajduje najbliższy nieodkryty kafelek geometrycznie.
		return TryFindNearestUnexploredBFS(center, out result);
	}
	/// <summary>
	/// Algorytm Breadth-First Search (BFS) szukający najbliższego nieodkrytego kafelka.
	/// </summary>
	private bool TryFindNearestUnexploredBFS(Vector3 startPos, out Vector3 result)
	{
		Vector3Int startCell = floorTilemap.WorldToCell(startPos);

		// Kolejka do BFS
		Queue<Vector3Int> queue = new Queue<Vector3Int>();
		// Zbiór odwiedzonych W TYM WYSZUKIWANIU
		HashSet<Vector3Int> visitedInSearch = new HashSet<Vector3Int>();

		queue.Enqueue(startCell);
		visitedInSearch.Add(startCell);

		int safetyCounter = 0;
		int maxIterations = 2000; // Limit iteracji dla wydajności

		Vector3Int[] directions = {
			new Vector3Int(0, 1, 0),  // Góra
			new Vector3Int(0, -1, 0), // Dół
			new Vector3Int(-1, 0, 0), // Lewo
			new Vector3Int(1, 0, 0)   // Prawo
		};

		while (queue.Count > 0 && safetyCounter < maxIterations)
		{
			Vector3Int current = queue.Dequeue();
			safetyCounter++;

			// Sprawdź czy to jest nasz cel (kafelek istnieje, ale nie jest w exploredTiles)
			if (floorTilemap.HasTile(current) && !exploredTiles.Contains(current))
			{
				result = floorTilemap.GetCellCenterWorld(current);
				return true;
			}

			// Sprawdzanie sąsiadów
			foreach (var dir in directions)
			{
				Vector3Int neighbor = current + dir;

				if (!visitedInSearch.Contains(neighbor) && floorTilemap.HasTile(neighbor))
				{
					visitedInSearch.Add(neighbor);
					queue.Enqueue(neighbor);
				}
			}
		}

		// Jeśli nic nie znaleziono
		result = Vector3.zero;
		return false;
	}
}
