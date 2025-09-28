using Core.Services;
using DOTS.Components.Player;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace DOTS.Systems.Player
{
	public partial class PlayerSystem : SystemBase
	{
		internal readonly IPlayerInputService _playerInput;
		protected override void OnCreate()
		{
		}
		protected override void OnUpdate()
		{
			foreach (var (playerData, input, velocity, localTransform, entity) in
						 SystemAPI.Query<RefRW<PlayerDataComponent>, RefRO<PlayerInputComponent>, RefRW<PhysicsVelocity>, RefRW<LocalTransform>>().WithEntityAccess())
			{
				var inputVector = input.ValueRO.Move;
				Debug.Log($"input: {inputVector}");

				bool isMoving = playerData.ValueRW.CurrentSpeed > 0.05f;
				bool isTryingToMove = math.lengthsq(inputVector) > 0f;

				if (isTryingToMove)
				{
					// Accelerate
					playerData.ValueRW.CurrentSpeed += playerData.ValueRW.MoveSpeed;
					playerData.ValueRW.CurrentSpeed = playerData.ValueRW.CurrentSpeed;
				}
				else if (isMoving)
				{
					// Decelerate
					playerData.ValueRW.CurrentSpeed -= (playerData.ValueRW.MoveSpeed);
					playerData.ValueRW.CurrentSpeed = math.max(playerData.ValueRW.CurrentSpeed, 0f);
				}

				// Last direction
				float2 lastDir = velocity.ValueRW.Linear.xy;
				if (math.lengthsq(lastDir) < 0.0001f) lastDir = new float2(0, 1);
				float2 moveDirection = isTryingToMove ? inputVector : math.normalize(lastDir);


				var moveVector = moveDirection * playerData.ValueRW.CurrentSpeed;
				velocity.ValueRW.Linear = new float3(moveVector.x, moveVector.y, velocity.ValueRW.Linear.z);

			}
		}
		protected override void OnDestroy()
		{
		}

	}
}
