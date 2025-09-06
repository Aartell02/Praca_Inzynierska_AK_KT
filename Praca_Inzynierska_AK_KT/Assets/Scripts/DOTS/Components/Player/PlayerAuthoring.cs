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
			AddComponentObject(entity, new PlayerModelComponent
			{
				Prefab = authoring.PlayerModelPrefab
			});
			AddComponent(entity, new PlayerStatsComponent
			{
				MoveSpeed = authoring.MoveSpeed,
				Health = authoring.Health
			});

		}
	}

	public struct PlayerInputComponent : IComponentData
	{
		public Vector2 inputVector;
	}

	public struct PlayerStatsComponent : IComponentData
	{
		public float MoveSpeed;
		public float Health;
		public float AttackDamage;
		public float AttackSpeed;
	}

	public class PlayerModelComponent : IComponentData
	{
		public GameObject Prefab;
	}

	public struct PlayerInitializeTag : IComponentData { }
}
