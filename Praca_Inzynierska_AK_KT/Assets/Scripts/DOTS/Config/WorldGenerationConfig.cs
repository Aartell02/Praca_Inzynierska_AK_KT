using UnityEngine;

namespace DOTS
{
    [CreateAssetMenu(fileName = "WorldGenerationConfig", menuName = "Config/DOTS/WorldGenerationConfig")]
    public class WorldGenerationConfig : ScriptableObject
    {
		[SerializeField]
		protected TilemapVisualizer tilemapVisualizer = null;

		[SerializeField]
		protected Vector2Int startPosition = Vector2Int.zero;

		[SerializeField]
		private int corridorLength = 14, corridorCount = 5;

		[SerializeField]
		[Range(0.1f, 1)]
		public float roomPercent = 0.8f;

		[SerializeField]
		protected RandomWalkData randomWalkParameters;
	}
}
