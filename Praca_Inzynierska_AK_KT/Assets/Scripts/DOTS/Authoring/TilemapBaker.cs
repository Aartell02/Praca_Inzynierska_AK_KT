using DOTS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DOTS.Authoring
{
	class TilemapBaker : MonoBehaviour
	{
		[SerializeField]
		private Tilemap Floor;

		[SerializeField]
		private Tilemap Wall;

		public void BakeTilemap()
		{
			var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

			foreach (var pos in Floor.cellBounds.allPositionsWithin)
			{
				var tile = Floor.GetTile(pos);
				if (tile != null) continue;

				var entity = entityManager.CreateEntity();
				entityManager.AddComponentData(entity, new TileData
				{
					CellPosition = pos,
				});

				entityManager.AddComponent<PhysicsCollider>(entity);

#if UNITY_EDITOR
				entityManager.SetName(entity, $"Tile_{pos.x}_{pos.y}");
#endif
			}
		}
	}

	public struct TileData : IComponentData
	{
		public Vector3 CellPosition;
	}
}
