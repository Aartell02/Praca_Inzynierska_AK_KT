using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class PlayerVisibleSensor : LocalWorldSensorBase
	{
		[SerializeField] private float visionRange = 10f;
		[SerializeField] private float visionAngle = 90f;
		public override void Created() { }
		public override void Update() { }

		public override SenseValue Sense(IActionReceiver agent, IComponentReference references)
		{
			var player = GameObject.FindGameObjectWithTag("Player");
			if (player == null)
				return false;

			var directionToPlayer = player.transform.position - agent.Transform.position;
			var distance = directionToPlayer.magnitude;

			// Check range
			if (distance > visionRange)
				return false;

			// Check vision cone (for 2D use Vector2 and forward = up/right)
			var angle = Vector3.Angle(agent.Transform.forward, directionToPlayer);
			if (angle > visionAngle / 2f)
				return false;

			// Optional: Raycast for line of sight
			if (Physics.Raycast(agent.Transform.position, directionToPlayer.normalized,
				out RaycastHit hit, distance))
			{
				if (hit.collider.CompareTag("Player"))
					return true;
			}

			return false;
		}
	}
}
