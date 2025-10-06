using DOTS.Components.Player;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTS.Authoring
{
	public class CameraFollowAuthoring : MonoBehaviour
	{
		GameObject Target;
		public float3 Offset;
		public float SmoothSpeed;

		[DisallowMultipleComponent]
		public class CameraFollowBaker : Baker<CameraFollowAuthoring>
		{
			public override void Bake(CameraFollowAuthoring authoring)
			{
				Entity entity = GetEntity(TransformUsageFlags.Dynamic);

				AddComponent(entity, new CameraParametersComponent
				{
					Offset = authoring.Offset,
					SmoothSpeed = authoring.SmoothSpeed,
				});
			}
		}
	}


}
