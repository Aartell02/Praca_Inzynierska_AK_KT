using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class ScoutCapabilityFactory : CapabilityFactoryBase, IInjectable
	{
		EnemyConfig enemyConfig;
		public override ICapabilityConfig Create()
		{
			var builder = new CapabilityBuilder("ScoutCapability");

			BuildGoals(builder);
			BuildActions(builder);
			BuildSensors(builder);

			return builder.Build();
		}

		void BuildGoals(CapabilityBuilder builder)
		{
			builder.AddGoal<GetCommandGoal>()
				.AddCondition<HasGoal>(Comparison.GreaterThanOrEqual, 1);
		}

		void BuildActions(CapabilityBuilder builder)
		{
			builder.AddAction<WaitForCommandsAction>()
				.AddCondition<CommanderInRange>(Comparison.GreaterThanOrEqual, 1)
				.SetRequiresTarget(false)
				.AddEffect<HasGoal>(EffectType.Increase)
				.SetBaseCost(1);

			builder.AddAction<WanderToTargetAction>()
				.SetTarget<CommanderTarget>()
				.AddEffect<CommanderInRange>(true)
				.SetMoveMode(ActionMoveMode.MoveBeforePerforming)
				.SetBaseCost(5)
				.SetStoppingDistance(enemyConfig.EnemyCommunicationData.CommunicationRadius);
		}

		void BuildSensors(CapabilityBuilder builder)
		{
			builder.AddTargetSensor<CommanderTargetSensor>().SetTarget<CommanderTarget>();
			// musze obsluzyc zmienna. effect mowi tylko dla planera na co wyplywa akcja a ja musze faktycznie zwiekszyc wartosc
			builder.AddWorldSensor<HasGoalSensor>().SetKey<HasGoal>();
			builder.AddWorldSensor<TargetInRangeSensor>().SetKey<CommanderInRange>();

		}

		public void Inject(DependencyInjector injector)
		{
			this.enemyConfig = injector.EnemyConfig;
		}
	}
}
