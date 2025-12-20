using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class StrategizeAction : GoapActionBase<StrategizeAction.Data>
	{
		// This method is called when the action is started
		// This method is optional and can be removed
		public override void Start(IMonoAgent agent, Data data)
		{
		}

		// This method is called every frame while the action is running
		// This method is required
		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			// Return continue to keep the action running
			return ActionRunState.Continue;
		}

		// The action class itself must be stateless!
		// All data should be stored in the data class
		public class Data : IActionData
		{
			public ITarget Target { get; set; }
		}
	}
}
