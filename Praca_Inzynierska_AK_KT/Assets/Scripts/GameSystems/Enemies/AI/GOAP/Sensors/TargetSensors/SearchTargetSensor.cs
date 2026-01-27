using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;
using UnityEngine.AI;

namespace GameSystems.AI
{
	// Defining a GoapId is only necessary when using the ScriptableObject configuration method.
	public class SearchTargetSensor : LocalTargetSensorBase, IInjectable
	{
		EnemyConfig enemyConfig;

		public override void Created() {}
		// Is called every frame that an agent of an `AgentType` that uses this sensor needs it.
		// This can be used to 'cache' data that is used in the `Sense` method.
		// Eg look up all the trees in the scene, and then find the closest one in the Sense method.
		public override void Update() { }

		public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
		{

			Vector3 result;
			ExplorationGrid.Instance.TryGetUnexploredTarget(agent.Transform.position, enemyConfig.EnemyCommunicationData.SensorRadius, out result);

			if (NavMesh.SamplePosition(result, out NavMeshHit hit, 0.2f, NavMesh.AllAreas))
			{
				Vector2 validPoint = hit.position;
				if (existingTarget is PositionTarget positionTarget)
				{
					return positionTarget.SetPosition(result);
				}
			}
			return new PositionTarget(agent.Transform.position);
		}

		public void Inject(DependencyInjector injector)
		{
			this.enemyConfig = injector.EnemyConfig;
		}
	}
}
