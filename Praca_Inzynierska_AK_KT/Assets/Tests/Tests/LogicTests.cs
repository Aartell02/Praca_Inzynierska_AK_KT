using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using GameSystems.MapGeneration;

public class LogicTests
{
	[Test]
	public void SimpleRandomWalk_GeneratesPath()
	{
		var result = ProceduralGenerationAlgorithm.SimpleRandomWalk(Vector2Int.zero, 10);

		Assert.IsTrue(result.Count > 1);
	}

	[Test]
	public void ItemPlacer_BlocksDoor()
	{
		var floors = new HashSet<Vector2Int> { Vector2Int.zero };
		var walls = new HashSet<Vector2Int>();
		var doors = new HashSet<Vector2Int> { Vector2Int.zero };
		var placed = new List<Vector2Int>();

		bool valid = ItemPlacer.IsPositionValid(
			Vector2Int.zero,
			floors,
			walls,
			doors,
			placed,
			1,
			1,
			1
		);

		Assert.IsFalse(valid);
	}
}
