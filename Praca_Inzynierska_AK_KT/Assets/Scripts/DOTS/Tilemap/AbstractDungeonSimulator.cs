using UnityEngine;

namespace DOTS
{
    public abstract class AbstractDungeonSimulator : MonoBehaviour
    {
		[SerializeField]
		protected TilemapVisualizer tilemapVisualizer = null;
		[SerializeField]
		protected Vector2Int startPosition = Vector2Int.zero;

		public void GenerateDungeon()
		{
			tilemapVisualizer.ClearTiles();
			RunProceduralGeneration();
		}

		protected abstract void RunProceduralGeneration();
    }
}
