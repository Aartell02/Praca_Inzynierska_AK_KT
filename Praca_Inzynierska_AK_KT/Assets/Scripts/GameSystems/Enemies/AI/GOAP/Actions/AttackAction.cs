using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using System;
using UnityEngine;

namespace GameSystems.AI
{
	public class AttackAction : GoapActionBase<AttackAction.Data>
	{
		private float attackCooldown = 1f;
		private float lastAttackTime;

		public override void Created() { }

		public override void BeforePerform(IMonoAgent agent, Data data)
		{
			lastAttackTime = Time.time;
			Debug.Log($"{agent.gameObject.name} begins attacking!");
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			// Attack every cooldown seconds
			if (Time.time - lastAttackTime >= attackCooldown)
			{
				// Deal damage
				Debug.Log($"{agent.gameObject.name} attacks player!");

				// Get player and deal damage
				var player = GameObject.FindGameObjectWithTag("Player");
				if (player != null)
				{
					// Example: player.GetComponent<PlayerHealth>()?.TakeDamage(10);
				}

				lastAttackTime = Time.time;
			}

			// Continue attacking while player is in range
			return ActionRunState.Continue;
		}

		public override void End(IMonoAgent agent, Data data)
		{
			Debug.Log($"{agent.gameObject.name} stopped attacking");
		}

		public class Data : IActionData
		{
			public ITarget Target { get; set; }
		}
	}

}
