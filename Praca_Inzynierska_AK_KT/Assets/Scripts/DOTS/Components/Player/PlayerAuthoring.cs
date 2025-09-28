using Unity.Entities;
using UnityEngine;

namespace DOTS.Components.Player
{
    public class PlayerAuthoring  : MonoBehaviour
    {
		[SerializeField]
		internal GameObject PlayerModelPrefab;

		[SerializeField]
		internal float MoveSpeed;

		[SerializeField]
		internal float Health = 0;
	}
	public class Baker : Baker<PlayerAuthoring>
	{
		public override void Bake(PlayerAuthoring authoring)
		{
			Entity entity = GetEntity(TransformUsageFlags.Dynamic);

			AddComponent<PlayerInitializeTag>(entity);
			AddComponent(entity, new PlayerModelComponent
			{
				Prefab = GetEntity(authoring.PlayerModelPrefab,TransformUsageFlags.None)
			});
			AddComponent(entity, new PlayerDataComponent
			{
				MoveSpeed = authoring.MoveSpeed,
				Health = authoring.Health
			});
			AddComponent<PlayerInputComponent>(entity);

		}
	}

	struct PlayerModelComponent : IComponentData
	{
		internal Entity Prefab;
	}

	struct PlayerInitializeTag : IComponentData { }
}
