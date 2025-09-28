using Core.Inspector;
using Core;
using Unity.Entities;
using UnityEngine;

namespace DOTS.Components
{
	public class EntityReferencesAuthoring : MonoBehaviour
	{
		[SerializeField]
		internal GameObject PlayerPrefab;

		[SerializeField]
		[EnumArray(typeof(EnemyType))]
		internal GameObject[] EnemiesPrefab;

		public class Baker : Baker<EntityReferencesAuthoring>
		{
			public override void Bake(EntityReferencesAuthoring authoring)
			{
				Entity entity = GetEntity(TransformUsageFlags.Dynamic);

				AddComponent(entity, new EntityReferences
				{
					playerReference = GetEntity(authoring.PlayerPrefab, TransformUsageFlags.Dynamic),
				});

				DynamicBuffer<EnemyReferenceBuffer> buffer = AddBuffer<EnemyReferenceBuffer>(entity);
				foreach (var enemyPrefab in authoring.EnemiesPrefab)
				{
					buffer.Add(new EnemyReferenceBuffer
					{
						Enemy = GetEntity(enemyPrefab, TransformUsageFlags.Dynamic)
					});
				}
			}
		}
	}

	internal struct EntityReferences : IComponentData
	{
		internal Entity playerReference;
	}

	internal struct EnemyReferenceBuffer : IBufferElementData
	{
		internal Entity Enemy;
	}
}
