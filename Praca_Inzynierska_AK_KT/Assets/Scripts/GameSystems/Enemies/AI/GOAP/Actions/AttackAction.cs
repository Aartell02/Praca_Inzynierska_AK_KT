using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using System;
using UnityEngine;

namespace GameSystems.AI
{
	public class AttackAction : GoapActionBase<AttackAction.Data>
	{
		[Serializable]
		public class Data : IActionData
		{
			public ITarget Target { get; set; }
		}

		public override void Created() { }
		public override bool IsValid(IActionReceiver agent, Data data) => true;
		public override void Start(IMonoAgent agent, Data data) { }

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			// Atakujemy od razu, akcję kończymy natychmiast (np. zadanie obrażeń)
			return ActionRunState.Completed;
		}

		public override void Complete(IMonoAgent agent, Data data)
		{
			// Po wykonaniu ataku niszczymy gracza (symulujemy śmierć)
			if (data.Target is TransformTarget tx && tx.Transform != null)
			{
				GameObject player = tx.Transform.gameObject;
				UnityEngine.Object.Destroy(player);
			}
		}
	}

}
