using DOTS.Authoring;
using DOTS.Components.Player;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace DOTS.Systems.Player
{
	[UpdateInGroup(typeof(SimulationSystemGroup))]
	public partial struct PlayerInitialization : ISystem
	{
		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate<PhysicsInitializeTag>();
		}

		public void OnUpdate(ref SystemState state)
		{
			var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

			foreach ((RefRW<PhysicsMass> physics, Entity entity)
				in SystemAPI.Query<RefRW<PhysicsMass>>().WithAll<PhysicsInitializeTag>().WithEntityAccess())
			{
				physics.ValueRW.InverseInertia = float3.zero;
				ecb.RemoveComponent<PhysicsInitializeTag>(entity);
			}

			ecb.Playback(state.EntityManager);
			ecb.Dispose();
		}
	}
}
