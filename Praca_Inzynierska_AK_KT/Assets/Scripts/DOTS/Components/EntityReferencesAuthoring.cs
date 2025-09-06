using DOTS.Components.Player;
using Unity.Entities;
using UnityEngine;

namespace DOTS.Components
{
	public class EntityReferencesAuthoring : MonoBehaviour
	{
		[SerializeField]
		internal GameObject PlayerPrefab;
		public class Baker : Baker<EntityReferencesAuthoring>
		{
			public override void Bake(EntityReferencesAuthoring authoring)
			{
				Entity entity = GetEntity(TransformUsageFlags.Dynamic);

				AddComponent(entity, new EntityReferences
				{
					playerReference = GetEntity(authoring.PlayerPrefab, TransformUsageFlags.Dynamic),
				});
			}
		}
	}

	internal struct EntityReferences : IComponentData
	{
		internal Entity playerReference;
	}
}
