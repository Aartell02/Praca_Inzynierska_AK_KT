using DOTS.Authoring;
using DOTS.Components;
using Unity.Entities;
using Unity.Mathematics;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

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

			var player = ecb.Instantiate(entityReferences.playerReference);

			var camera = ecb.Instantiate(entityReferences.cameraReference);


			CameraSingleton cameraSingleton = CameraSingleton.Instance;
			ecb.AddComponent(camera, new CameraTargetComponent
			{
				Target = player,
				CameraTransform = cameraSingleton.transform
			});

#if UNITY_EDITOR
			ecb.SetName(player, $"Player");
#endif
			ecb.Playback(state.EntityManager);
			ecb.Dispose();


			state.Enabled = false;
		}
	}
}
