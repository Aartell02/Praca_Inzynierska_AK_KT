using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTS.Components
{
	struct TilemapData : IComponentData
	{
		internal int Width;
		internal int Height;
		internal BlobAssetReference<TileTypeBlob> TileTypes;
	}

	struct TileTypeBlob
	{
		internal BlobArray<int> Tiles;
	}
}

