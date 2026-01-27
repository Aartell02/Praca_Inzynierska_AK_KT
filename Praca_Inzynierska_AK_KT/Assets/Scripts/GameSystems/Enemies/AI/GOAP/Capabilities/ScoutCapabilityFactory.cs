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
			builder.AddGoal<DeliverPillarLocationsGoal>()
				.AddCondition<HasGoal>(Comparison.SmallerThan, 1)
				.AddCondition<InfoPoints>(Comparison.GreaterThanOrEqual, 1);
		}

		void BuildActions(CapabilityBuilder builder)
		{
			builder.AddAction<SearchForTargetAction>()
				.SetTarget<SearchTarget>()
				.AddEffect<InfoPoints>(EffectType.Increase)
				.SetBaseCost(1)
				.SetStoppingDistance(2f);

			builder.AddAction<ReportInformationAction>()
				.AddCondition<InfoPoints>(Comparison.GreaterThanOrEqual, 1)
				.SetTarget<CommanderTarget>()
				.AddEffect<HasGoal>(EffectType.Decrease)
				.SetBaseCost(1)
				.SetMoveMode(ActionMoveMode.MoveBeforePerforming)
				.SetStoppingDistance(enemyConfig.EnemyCommunicationData.CommunicationRadius - 1);
		}

		void BuildSensors(CapabilityBuilder builder)
		{
			builder.AddTargetSensor<SearchTargetSensor>().SetTarget<SearchTarget>();
			builder.AddWorldSensor<InfoPointsSensor>().SetKey<InfoPoints>();

		}

		public void Inject(DependencyInjector injector)
		{
			this.enemyConfig = injector.EnemyConfig;
		}
	}
}
