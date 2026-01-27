using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class StrategizeAction : GoapActionBase<StrategizeAction.Data>
	{
		public class Data : IActionData
		{
			public ITarget Target { get; set; }
			public float Timer { get; set; }
		}

		public override void Start(IMonoAgent agent, Data data)
		{
			data.Timer = 1.5f;
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			data.Timer -= context.DeltaTime;
			return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Stop;
		}
	}
}
