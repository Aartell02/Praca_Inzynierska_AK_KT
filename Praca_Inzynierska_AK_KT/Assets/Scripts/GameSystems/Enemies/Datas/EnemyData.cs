using Core;
using CrashKonijn.Agent.Runtime;
using UnityEngine;
using UnityEngine.AI;

namespace GameSystems
{
    abstract class EnemyData : MonoBehaviour
    {
		[SerializeField]
		internal EnemyType EnemyType;
		[SerializeField]
		internal int Heatlh;
		[SerializeField]
		internal float MoveSpeed;
		[SerializeField]
		internal AIEnemyState Goal;
		[SerializeField]
		internal bool TargetInRange;

		internal Animator Animator;
		internal Rigidbody2D Rigidbody;
		internal SpriteRenderer SpriteRenderer;


		private void Awake()
		{
			this.Animator = this.GetComponent<Animator>();
			this.Rigidbody = this.GetComponent<Rigidbody2D>();
			this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
		}
	}
}
