using Unity.Entities;

namespace DOTS.Components.Player
{
	struct PlayerDataComponent : IComponentData
	{
		internal float Health;
		internal float AttackDamage;
		internal float AttackSpeed;

		internal float MoveSpeed;
		internal float CurrentMoveSpeed;
		internal float MaxMoveSpeed;
	}
}
