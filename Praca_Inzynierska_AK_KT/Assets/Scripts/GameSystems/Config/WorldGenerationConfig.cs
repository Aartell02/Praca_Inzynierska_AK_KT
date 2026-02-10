using GameSystems.Data;
using UnityEngine;

namespace GameSystems.Config
{
    [CreateAssetMenu(fileName = "WorldGenerationConfig", menuName = "Config/WorldGenerationConfig")]
    public class WorldGenerationConfig : ScriptableObject
    {
		[SerializeField]
		public Vector2Int startPosition = Vector2Int.zero;

		[SerializeField]
		public int corridorLength = 14;

		[SerializeField]
		public int corridorCount = 0;

		[SerializeField]
		[Range(0.1f, 1)]
		public float roomPercent = 0.8f;

		[SerializeField]
		public RandomWalkData randomWalkParameters;
	}
}
