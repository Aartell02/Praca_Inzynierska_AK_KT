using DOTS.Authoring;
using DOTS.Components.Player;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace DOTS.Systems.Player
{
	[UpdateInGroup(typeof(SimulationSystemGroup))]
	public partial struct CameraFollowSystem : ISystem
	{
		public void OnUpdate(ref SystemState state)
		{
			foreach ((RefRW<CameraTargetComponent> camera, RefRO<CameraParametersComponent> parameters) in
					 SystemAPI.Query<RefRW<CameraTargetComponent>,RefRO<CameraParametersComponent>>())
			{
				var targetEntity = camera.ValueRO.Target;

				if (targetEntity == Entity.Null)
					continue;

				var targetTransform = SystemAPI.GetComponent<LocalTransform>(targetEntity);
				float3 targetPos = targetTransform.Position;

				float3 desiredPos = new float3(
					targetPos.x + parameters.ValueRO.Offset.x,
					targetPos.y + parameters.ValueRO.Offset.y,
					parameters.ValueRO.Offset.z
				);

				camera.ValueRW.CameraTransform.Value.position = desiredPos;
			}
		}
	}
}
