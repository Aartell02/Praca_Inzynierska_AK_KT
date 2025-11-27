using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class PlayerVisibleSensor : LocalWorldSensorBase
	{
		public float visionRange = 10f;
		public override void Created() { }
		public override void Update() { }

		public override SenseValue Sense(IActionReceiver agent, IComponentReference references)
		{
			Transform agentTransform = agent.Transform;
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if (player == null) return false;

			// Sprawdź odległość w 2D
			float dist = Vector2.Distance(agentTransform.position, player.transform.position);
			if (dist > visionRange) return false;
			// Opcjonalnie: sprawdź czy raycast nie trafia w przeszkodę
			RaycastHit2D hit = Physics2D.Raycast(agentTransform.position, (player.transform.position - agentTransform.position).normalized, visionRange);
			return (hit.collider != null && hit.collider.CompareTag("Player"));
		}
	}
}
