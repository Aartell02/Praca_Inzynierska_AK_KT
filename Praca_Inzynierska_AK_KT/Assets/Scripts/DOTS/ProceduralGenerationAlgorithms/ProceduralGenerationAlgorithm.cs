using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace DOTS
{
    public static class ProceduralGenerationAlgorithm
    {
		public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
		{
			HashSet<Vector2Int> path = new HashSet<Vector2Int>();

			path.Add(startPosition);
			var previousposition = startPosition;

			for (int i = 0; i < walkLength; i++)
			{
				var newPosition = previousposition + Direction2D.GetRandomCardinalDirection();
				path.Add(newPosition);
				previousposition = newPosition;
			}

			return path;
		}
    }

	public static class Direction2D
	{
		public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
		{
			new Vector2Int(0,1), //UP
			new Vector2Int(1,0), //RIGHT
			new Vector2Int(0,-1), //DOWN
			new Vector2Int(-1,0) //LEFT
		};

		public static Vector2Int GetRandomCardinalDirection()
		{
			return cardinalDirectionsList [Random.Range(0, cardinalDirectionsList.Count)];
		}
	}
}
