using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class StrategizeAction : GoapActionBase<StrategizeAction.Data>
	{
		public override void Start(IMonoAgent agent, Data data)
		{
			data.commanderData = agent.GetComponent<CommanderBrainBehaviour>();
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{

			return ActionRunState.Continue;
		}

		public class Data : IActionData
		{
			public CommanderBrainBehaviour commanderData { get; set; }
			public ITarget Target { get; set; }
		}
	}
}
