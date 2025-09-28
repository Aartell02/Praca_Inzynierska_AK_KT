using Unity.Entities;
using UnityEngine;

namespace DOTS.Components.Player
{
	struct PlayerInputComponent : IComponentData
	{
		public Vector2 Move;
	}
}
