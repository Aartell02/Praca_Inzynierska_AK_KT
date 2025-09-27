using DOTS.Components;
using Unity.Entities;
using Gameplay;
using Core;

namespace DOTS.Systems.Enemies
{
	[UpdateInGroup(typeof(SimulationSystemGroup))]
	public partial struct EnemySpawnSystem : ISystem
	{
		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate<EntityReferences>();
		}

		public void OnUpdate(ref SystemState state)
		{
			Entity entity = SystemAPI.GetSingletonEntity<EntityReferences>();

			EntityReferences entityReferences = SystemAPI.GetComponent<EntityReferences>(entity);

			DynamicBuffer<EnemyReferenceBuffer> enemyBuffer = SystemAPI.GetBuffer<EnemyReferenceBuffer>(entity);

			var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

			var enemyConfig = GameplayViewModel.Instance.GetConfig();

			foreach (var enemyData in enemyConfig.EnemySpawnData)
			{
				switch (enemyData.Type)
				{
					case EnemyType.Scout:
						for (int i = 0; i < enemyData.Count; i++)
							ecb.Instantiate(enemyBuffer[(int)enemyData.Type].Enemy);
						break;
				}
			}

			ecb.Playback(state.EntityManager);
			ecb.Dispose();

			state.Enabled = false;
		}
	}
}
