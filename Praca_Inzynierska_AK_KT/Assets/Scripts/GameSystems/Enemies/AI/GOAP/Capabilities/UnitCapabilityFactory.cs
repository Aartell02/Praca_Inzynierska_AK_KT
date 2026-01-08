using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class UnitCapabilityFactory : CapabilityFactoryBase, IInjectable
	{
		EnemyConfig enemyConfig;
		public override ICapabilityConfig Create()
		{
			var builder = new CapabilityBuilder("UnitCapability");

			BuildGoals(builder);
			BuildActions(builder);
			BuildSensors(builder);

			return builder.Build();
		}

		void BuildGoals(CapabilityBuilder builder)
		{
			builder.AddGoal<GetOrderGoal>()
				.AddCondition<HasGoal>(Comparison.GreaterThanOrEqual, 1)
				.SetBaseCost(1);
		}

		void BuildActions(CapabilityBuilder builder)
		{
			builder.AddAction<WaitForCommandsAction>()
				.SetTarget<CommanderTarget>()
				.AddEffect<HasGoal>(EffectType.Increase)
				.SetBaseCost(1)
				.SetMoveMode(ActionMoveMode.MoveBeforePerforming)
				.SetStoppingDistance(enemyConfig.EnemyCommunicationData.CommunicationRadius - 1);

			builder.AddAction<GoToPositionAction>()
				.SetTarget<UnitPositionTarget>()
				.AddEffect<HasGoal>(EffectType.Decrease)
				.SetBaseCost(2)
				.SetMoveMode(ActionMoveMode.MoveBeforePerforming)
				.SetStoppingDistance(enemyConfig.EnemyCommunicationData.CommunicationRadius - 1);
		}

		void BuildSensors(CapabilityBuilder builder)
		{
			builder.AddTargetSensor<CommanderTargetSensor>().SetTarget<CommanderTarget>();
			builder.AddTargetSensor<UnitPositionTargetSensor>().SetTarget<UnitPositionTarget>();
			builder.AddWorldSensor<HasOrderSensor>().SetKey<HasGoal>();
		}

		public void Inject(DependencyInjector injector)
		{
			this.enemyConfig = injector.EnemyConfig;
		}
	}
}
