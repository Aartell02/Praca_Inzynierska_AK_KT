using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using UnityEngine;
using UnityEngine.AI;

namespace GameSystems.AI
{
	public class AgentMoveBehaviour : MonoBehaviour
	{
		private NavMeshAgent navigationAgent;
		private AgentBehaviour agent;

		private ITarget currentTarget;
		private bool shouldMove;
		private Vector3 lastTargetPosition;
		private float updateThreshold = 0.5f;
		private EnemyData enemyData;
		private float currentMoveSpeed;

		private void Awake()
		{
			this.agent = this.GetComponent<AgentBehaviour>();
			this.navigationAgent = this.GetComponent<NavMeshAgent>();
			this.enemyData = this.GetComponent<EnemyData>();

			navigationAgent.updateRotation = false;
			navigationAgent.updateUpAxis = false;
		}

		private void OnEnable()
		{
			this.agent.Events.OnTargetInRange += this.OnTargetInRange;
			this.agent.Events.OnTargetChanged += this.OnTargetChanged;
			this.agent.Events.OnTargetNotInRange += this.TargetNotInRange;
			this.agent.Events.OnTargetLost += this.TargetLost;
		}

		private void OnDisable()
		{
			this.agent.Events.OnTargetInRange -= this.OnTargetInRange;
			this.agent.Events.OnTargetChanged -= this.OnTargetChanged;
			this.agent.Events.OnTargetNotInRange -= this.TargetNotInRange;
			this.agent.Events.OnTargetLost -= this.TargetLost;
		}

		private void TargetLost()
		{
			this.currentTarget = null;
			this.shouldMove = false;
		}

		private void OnTargetInRange(ITarget target)
		{
			this.shouldMove = false;
		}

		private void OnTargetChanged(ITarget target, bool inRange)
		{
			this.currentTarget = target;
			if (currentTarget != null)
			{
				navigationAgent.SetDestination(currentTarget.Position);
			}

			this.shouldMove = !inRange;
		}

		private void TargetNotInRange(ITarget target)
		{
			this.shouldMove = true;
		}

		public void Update()
		{
			Debug.Log($"{agent.name} update");
			if (this.agent.IsPaused)
				return;

			Vector3 vel = navigationAgent.velocity;

			if (vel.x > 0.01f)
				enemyData.SpriteRenderer.flipX = false;
			else if (vel.x < -0.01f)
				enemyData.SpriteRenderer.flipX = true;

			if (!shouldMove)
			{
				if (enemyData.EnemyType != EnemyType.Scout)
				{
					enemyData.Animator.SetFloat("Move", 0);
				}
				return;
			}

			if (currentTarget == null)
			{
				navigationAgent.ResetPath();
				return;
			}

			if (Vector3.Distance(currentTarget.Position, lastTargetPosition) > updateThreshold)
			{
				navigationAgent.SetDestination(currentTarget.Position);
				lastTargetPosition = currentTarget.Position;
			}

			if (enemyData.EnemyType != EnemyType.Scout)
			{
				enemyData.Animator.SetFloat("Move", 1);
			}
		}

		private void OnDrawGizmos()
		{
			if (this.currentTarget == null)
				return;

			Gizmos.DrawLine(this.transform.position, this.currentTarget.Position);
		}
	}
}
