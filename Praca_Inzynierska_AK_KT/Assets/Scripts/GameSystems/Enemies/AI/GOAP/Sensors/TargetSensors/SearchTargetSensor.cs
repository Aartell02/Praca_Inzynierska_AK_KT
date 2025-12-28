using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;
using UnityEngine.AI;

namespace GameSystems.AI
{
	// Defining a GoapId is only necessary when using the ScriptableObject configuration method.
	public class SearchTargetSensor : LocalTargetSensorBase
	{
		public override void Created()
		{
		}

		// Is called every frame that an agent of an `AgentType` that uses this sensor needs it.
		// This can be used to 'cache' data that is used in the `Sense` method.
		// Eg look up all the trees in the scene, and then find the closest one in the Sense method.
		public override void Update() { }

		public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
		{

			var random = this.GetRandomPosition(agent);

			if (NavMesh.SamplePosition(random, out NavMeshHit hit, 0.2f, NavMesh.AllAreas))
			{
				Vector2 validPoint = hit.position;
				// If the existing target is a `PositionTarget`, we can reuse it and just update the position.
				if (existingTarget is PositionTarget positionTarget)
				{
					return positionTarget.SetPosition(random);
				}

			}

			return new PositionTarget(agent.Transform.position);
		}

		private Vector3 GetRandomPosition(IActionReceiver agent)
		{
			Bounds Bounds = new(agent.Transform.position, new Vector2(5, 5));
			var random = Random.insideUnitCircle * 3f;
			var position = agent.Transform.position + new Vector3(random.x, random.y, 0);

			// Check if the position is within the bounds of the world.
			if (Bounds.Contains(position))
				return position;

			return Bounds.ClosestPoint(position);
		}
	}
}
