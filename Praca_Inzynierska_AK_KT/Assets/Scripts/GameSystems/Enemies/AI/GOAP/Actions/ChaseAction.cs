using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using System;
using UnityEngine;

namespace GameSystems.AI
{
    public class ChaseAction : GoapActionBase<ChaseAction.Data>
	{
		public override void Created() { }

		public override void BeforePerform(IMonoAgent agent, Data data)
		{
			// Called once when action starts
			Debug.Log($"{agent.gameObject.name} starts chasing player!");
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			// The agent automatically moves toward the target
			// Check if we're close enough
			var distanceToPlayer = Vector3.Distance(
				agent.Transform.position,
				data.Target.Position
			);

			// Complete when close enough (within stopping distance)
			if (distanceToPlayer < 2f)
				return ActionRunState.Completed;

			return ActionRunState.Continue;
		}

		public override void End(IMonoAgent agent, Data data)
		{
			Debug.Log($"{agent.gameObject.name} finished chasing");
		}

		public class Data : IActionData
		{
			public ITarget Target { get; set; }
		}
	}
}
