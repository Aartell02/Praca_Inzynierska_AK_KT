using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class CombatCapabilityFactory : CapabilityFactoryBase
	{
		public override ICapabilityConfig Create()
		{
			var builder = new CapabilityBuilder("CombatCapability");

			// Cel: zabić gracza (czyli PlayerAlive == 0)
			builder.AddGoal<KillPlayerGoal>()
				   .AddCondition<PlayerAlive>(Comparison.SmallerThanOrEqual, 0)
				   .SetBaseCost(10);

			// Akcja: gonić gracza (jeśli widoczny i poza zasięgiem)
			builder.AddAction<ChaseAction>()
				   .AddCondition<PlayerVisible>(Comparison.GreaterThanOrEqual, 1)
				   .AddCondition<PlayerInRange>(Comparison.SmallerThanOrEqual, 0)
				   .SetTarget<PlayerTarget>()
				   .SetMoveMode(ActionMoveMode.PerformWhileMoving)
				   .SetStoppingDistance(1.0f)
				   .SetBaseCost(2);

			// Akcja: atakować gracza (jeśli widoczny i w zasięgu)
			builder.AddAction<AttackAction>()
				   .AddCondition<PlayerVisible>(Comparison.GreaterThanOrEqual, 1)
				   .AddCondition<PlayerInRange>(Comparison.GreaterThanOrEqual, 1)
				   .AddEffect<PlayerAlive>(EffectType.Decrease)
				   .SetTarget<PlayerTarget>()
				   .SetMoveMode(ActionMoveMode.MoveBeforePerforming)
				   .SetStoppingDistance(0.5f)
				   .SetBaseCost(1);

			// Sensory dla zdolności
			builder.AddWorldSensor<PlayerVisibleSensor>().SetKey<PlayerVisible>();
			builder.AddWorldSensor<PlayerInRangeSensor>().SetKey<PlayerInRange>();
			builder.AddWorldSensor<PlayerAliveSensor>().SetKey<PlayerAlive>();
			builder.AddTargetSensor<PlayerTargetSensor>().SetTarget<PlayerTarget>();

			return builder.Build();
		}
	}
}
