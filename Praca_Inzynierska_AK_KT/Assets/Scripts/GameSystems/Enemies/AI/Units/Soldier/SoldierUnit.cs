using UnityEngine;
using NPBehave;

namespace GameSystems
{
	public class SoldierUnit : MonoBehaviour
	{
		private Blackboard blackboard;
		private Root behaviorTree;

		public float detectionRange = 7.5f;
		public float moveSpeed = 2f;

		void Start()
		{
			behaviorTree = CreateBehaviourTree();
			blackboard = behaviorTree.Blackboard;

#if UNITY_EDITOR
			// Debugger tylko w edytorze
			Debugger debugger = (Debugger)this.gameObject.AddComponent(typeof(Debugger)) as Debugger;
			debugger.BehaviorTree = behaviorTree;
#endif

			behaviorTree.Start();
		}

		private void UpdatePlayerData()
		{
			GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
			if (playerGO == null) return;

			Vector3 playerPos = playerGO.transform.position;
			blackboard["playerPosition"] = playerPos;
			blackboard["playerDistance"] = Vector3.Distance(transform.position, playerPos);
		}

		private void MoveTowards(Vector3 targetPosition)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
		}

		private void SetColor(Color color)
		{
			var sr = GetComponent<SpriteRenderer>();
			if (sr != null)
			{
				sr.color = color;
			}
		}

		private Root CreateBehaviourTree()
		{
			return new Root(
				new Service(0.125f, UpdatePlayerData,
					new Selector(
						new BlackboardCondition("playerDistance", Operator.IS_SMALLER, detectionRange, Stops.IMMEDIATE_RESTART,
							new Sequence(
								new Action(() => SetColor(Color.red)) { Label = "Change to Red" },
								new Action(() =>
								{
									MoveTowards(blackboard.Get<Vector3>("playerPosition"));
								})
								{ Label = "Follow Player" }
							)
						),
						new Action(() =>
						{
							SetColor(Color.grey);
						})
						{ Label = "Idle/Patrol" }
					)
				)
			);
		}
	}
}
