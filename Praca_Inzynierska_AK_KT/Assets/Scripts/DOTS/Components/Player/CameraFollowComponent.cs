using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTS.Components.Player
{
	public struct CameraTargetComponent : IComponentData
	{
		public Entity Target;
		public UnityObjectRef<Transform> CameraTransform;
	}

	public struct CameraParametersComponent : IComponentData
	{
		public float3 Offset;
		public float SmoothSpeed;
	}
}

