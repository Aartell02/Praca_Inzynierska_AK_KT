using Core.Services;
using DOTS.Components.Player;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace DOTS.Systems.Player
{
	partial class PlayerMovementSystem : SystemBase
	{
		protected override void OnCreate()
		{
		}
		protected override void OnUpdate()
		{
			foreach (var (playerData, input, velocity, localTransform, entity) in
						 SystemAPI.Query<RefRW<PlayerDataComponent>, RefRW<PlayerInputComponent>, RefRW<PhysicsVelocity>, RefRW<LocalTransform>>().WithEntityAccess())
			{
				bool isMoving = playerData.ValueRW.CurrentMoveSpeed > 0.05f;
				bool isTryingToMove = math.lengthsq(input.ValueRO.Move) > 0f;

				if (isTryingToMove)
				{
					// Accelerate
					playerData.ValueRW.CurrentMoveSpeed += playerData.ValueRW.MoveSpeed;
					playerData.ValueRW.CurrentMoveSpeed = math.min(playerData.ValueRW.CurrentMoveSpeed, playerData.ValueRW.MaxMoveSpeed);
				}
				else if (isMoving)
				{
					// Decelerate
					playerData.ValueRW.CurrentMoveSpeed -= (playerData.ValueRW.MoveSpeed);
					playerData.ValueRW.CurrentMoveSpeed = math.max(playerData.ValueRW.CurrentMoveSpeed, 0f);
				}

				// Last direction
				float2 lastDir = velocity.ValueRW.Linear.xy;
				if (math.lengthsq(lastDir) < 0.0001f) lastDir = new float2(0, 1);
				float2 moveDirection = isTryingToMove ? input.ValueRO.Move : math.normalize(lastDir);


				var moveVector = moveDirection * playerData.ValueRW.CurrentMoveSpeed;
				velocity.ValueRW.Linear = new float3(moveVector.x, moveVector.y, velocity.ValueRW.Linear.z);



			}
		}
		protected override void OnDestroy()
		{
		}

	}
}
