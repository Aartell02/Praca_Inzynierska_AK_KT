using Unity.Entities;
using UnityEngine;

namespace DOTS.Components
{
	struct PlayerInputComponent : IComponentData
	{
		internal Vector2 Move;

		internal Vector2 MousePosition;
	}
}
