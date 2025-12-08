using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using System;
using UnityEngine;

namespace GameSystems.AI
{
	public class ListenToCommands : GoapActionBase<ListenToCommands.Data>
	{
		public class Data : IActionData
		{
			public float Timer;
			public ITarget Target { get; set; }
		}

		public override void Start (IMonoAgent agent, Data data)
		{
			Debug.Log($"{agent.gameObject.name} started listening to commands");
		}

		public override IActionRunState Perform (IMonoAgent agent, Data data, IActionContext context)
		{
			return ActionRunState.Continue;
		}

		public override void End(IMonoAgent agent, Data data)
		{
			Debug.Log($"{agent.gameObject.name} stopped listening");
		}

	}
}
