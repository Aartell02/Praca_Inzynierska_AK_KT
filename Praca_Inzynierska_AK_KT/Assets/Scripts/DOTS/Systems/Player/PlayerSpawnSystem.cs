using Unity.Entities;
using UnityEngine;
using Core.Services;
using Unity.Collections;
using DOTS.Components;
using Unity.Transforms;

namespace DOTS.Systems.Player
{
	[UpdateInGroup(typeof(SimulationSystemGroup))]
	public partial struct PlayerSpawnSystem : ISystem
	{
		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate<EntityReferences>();
		}

		public void OnUpdate(ref SystemState state)
		{
			EntityReferences entityReferences = SystemAPI.GetSingleton<EntityReferences>();

			var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

			ecb.Instantiate(entityReferences.playerReference);

			ecb.Playback(state.EntityManager);
			ecb.Dispose();

			state.Enabled = false;
		}
	}
}
