using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class DependencyInjector : GoapConfigInitializerBase, IGoapInjector
	{
		public EnemyConfig EnemyConfig;
		public override void InitConfig(IGoapConfig config)
		{
			config.GoapInjector = this;
		}

		public void Inject(IAction action)
		{
			if (action is IInjectable injectable)
			{
				injectable.Inject(this);
			}
		}

		public void Inject(IGoal goal)
		{
			if (goal is IInjectable injectable)
			{
				injectable.Inject(this);
			}
		}

		public void Inject(ISensor sensor)
		{
			if (sensor is IInjectable injectable)
			{
				injectable.Inject(this);
			}
		}

		public void Inject(IAgentTypeFactory factory)
		{
			if (factory is IInjectable injectable)
			{
				injectable.Inject(this);
			}
		}

		public void Inject(ICapabilityFactory factory)
		{
			if (factory is IInjectable injectable)
			{
				injectable.Inject(this);
			}
		}
	}
}
