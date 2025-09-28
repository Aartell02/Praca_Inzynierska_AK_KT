using Unity.Entities;

namespace DOTS.Components.Player
{
	struct PlayerDataComponent : IComponentData
	{
		internal float MoveSpeed;
		internal float Health;
		internal float AttackDamage;
		internal float AttackSpeed;

		internal float CurrentSpeed;
	}
}
