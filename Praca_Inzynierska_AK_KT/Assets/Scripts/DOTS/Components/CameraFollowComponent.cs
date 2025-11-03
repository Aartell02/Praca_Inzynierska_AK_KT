using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTS.Components
{
	struct CameraTargetComponent : IComponentData
	{
		internal Entity Target;
		internal UnityObjectRef<Transform> CameraTransform;
	}

	struct CameraParametersComponent : IComponentData
	{
		internal float3 Offset;
		internal float SmoothSpeed;
	}
}

