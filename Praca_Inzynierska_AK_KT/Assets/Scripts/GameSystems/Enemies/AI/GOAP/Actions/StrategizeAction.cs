using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class StrategizeAction : GoapActionBase<StrategizeAction.Data>
	{
		public override void Start(IMonoAgent agent, Data data)
		{
			data.commanderData = agent.GetComponent<CommanderData>();
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{

			return ActionRunState.Continue;
		}

		public class Data : IActionData
		{
			public CommanderData commanderData { get; set; }
			public ITarget Target { get; set; }
		}
	}
}
