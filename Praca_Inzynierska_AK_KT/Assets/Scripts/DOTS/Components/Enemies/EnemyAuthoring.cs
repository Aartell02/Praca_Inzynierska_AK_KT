using Core;
using Unity.Entities;
using UnityEngine;

namespace DOTS.Components.Enemies
{
	public class EnemyAuthoring : MonoBehaviour
	{
		[SerializeField]
		internal GameObject EntityModelPrefab;

		[SerializeField]
		internal EnemyType EnemyType;

		[SerializeField]
		internal float MoveSpeed;

		[SerializeField]
		internal float Health = 0;
	}
	public class Baker : Baker<EnemyAuthoring>
	{
		public override void Bake(EnemyAuthoring authoring)
		{
			Entity entity = GetEntity(TransformUsageFlags.Dynamic);
			AddComponent(entity, new EnemyTag
			{
				Type = authoring.EnemyType
			});
		}
	}

	public struct EnemyTag : IComponentData
	{
		public EnemyType Type;
	}

}
