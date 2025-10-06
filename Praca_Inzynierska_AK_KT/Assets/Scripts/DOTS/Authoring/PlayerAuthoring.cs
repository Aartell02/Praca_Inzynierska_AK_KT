using DOTS.Components.Player;
using Unity.Entities;
using UnityEngine;

namespace DOTS.Authoring
{
    public class PlayerAuthoring  : MonoBehaviour
    {
		[SerializeField]
		internal GameObject PlayerModelPrefab;

		[SerializeField]
		internal float MoveSpeed;

		[SerializeField]
		internal float Health = 0;

		[DisallowMultipleComponent]
		public class Baker : Baker<PlayerAuthoring>
		{
			public override void Bake(PlayerAuthoring authoring)
			{
				Entity entity = GetEntity(TransformUsageFlags.Dynamic);

				AddComponent<PlayerInitializeTag>(entity);
				AddComponent(entity, new PlayerModelComponent
				{
					Prefab = GetEntity(authoring.PlayerModelPrefab, TransformUsageFlags.None)
				});
				AddComponent(entity, new PlayerDataComponent
				{
					MoveSpeed = authoring.MoveSpeed,
					MaxMoveSpeed = authoring.MoveSpeed * 5,
					Health = authoring.Health
				});
				AddComponent<PlayerInputComponent>(entity);

				AddComponent<PhysicsInitializeTag>(entity);
			}
		}
	}

	struct PlayerModelComponent : IComponentData
	{
		internal Entity Prefab;
	}

	struct PlayerInitializeTag : IComponentData { }
}
