using Core;
using DOTS.Authoring;
using DOTS.Systems.Enemies;
using Unity.Entities;
using Unity.Plastic.Antlr3.Runtime;

namespace DOTS
{
    public static class DOTSViewModel
    {
		public static void BakeTilemap()
		{

			var baker = TilemapBaker.FindAnyObjectByType<TilemapBaker>();
			baker.BakeTilemap();
		}


		public static void SpawnEnemy(EnemyType enemyType, int count)
		{
			var world = World.DefaultGameObjectInjectionWorld;
			var entityManager = world.EntityManager;

			EntityQuery query = entityManager.CreateEntityQuery(typeof(EntityReferences));

			if (query.IsEmptyIgnoreFilter)
			{
				query.Dispose();
				return;
			}

			Entity entity = query.GetSingletonEntity();

			var enemyBuffer = entityManager.GetBuffer<EnemyReferenceBuffer>(entity);

			var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

			switch (enemyType)
			{
				case EnemyType.Scout:
					for (int i = 0; i < count; i++)
					{
						Entity newEnemy = ecb.Instantiate(enemyBuffer[(int)enemyType].Enemy);
					}
					break;
			}

			ecb.Playback(entityManager);
			ecb.Dispose();
		}
	}
}

