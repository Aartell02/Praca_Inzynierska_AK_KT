using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using System;
using UnityEngine;

namespace GameSystems.AI
{
    public class ChaseAction : GoapActionBase<ChaseAction.Data>
    {
		// The action class itself must be stateless!
		// All data should be stored in the data class
		[Serializable]
		public class Data : IActionData
		{
			public ITarget Target { get; set; }
			[GetComponent] public Rigidbody2D Body { get; set; }
		}

		public float speed = 2f;              // prędkość poruszania się

		public override void Created() { }
		public override bool IsValid(IActionReceiver agent, Data data) => true;
		public override void Start(IMonoAgent agent, Data data) { }
		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			// Jeśli nie ma celu (gracz zginął), kończmy akcję
			if (data.Target == null) return ActionRunState.Completed;

			// Aktualizuj cel do gracza
			if (data.Target is TransformTarget tx)
			{
				Vector3 playerPos = tx.Transform.position;
				Vector3 myPos = agent.Transform.position;
				float dist = Vector3.Distance(myPos, playerPos);
				// Jeśli jesteśmy poza zasięgiem zatrzymania, ruszamy

				if (dist > 1f)
				{
					Vector2 dir = (playerPos - myPos).normalized;
					data.Body.linearVelocity = dir * speed;
					return ActionRunState.Continue;
				}
			}
			// jeśli jesteśmy blisko wystarczająco, kończymy akcję
			data.Body.linearVelocity = Vector2.zero;
			return ActionRunState.Completed;
		}
		public override void Complete(IMonoAgent agent, Data data)
		{
			// Można tu wykonać dodatkowe akcje po zakończeniu (np. ustawić animację)
			data.Body.linearVelocity = Vector2.zero;
		}
	}
}
