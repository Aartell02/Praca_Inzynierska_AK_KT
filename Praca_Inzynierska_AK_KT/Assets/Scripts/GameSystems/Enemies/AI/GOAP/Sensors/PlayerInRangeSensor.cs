using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class PlayerInRangeSensor : LocalWorldSensorBase
	{
		public float attackRange = 1.5f;
		public override void Created() { }
		public override void Update() { }

		public override SenseValue Sense(IActionReceiver agent, IComponentReference references)
		{
			Transform agentTransform = agent.Transform;
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if (player == null) return false;

			float dist = Vector2.Distance(agentTransform.position, player.transform.position);
			return (dist <= attackRange);
		}
	}
}
