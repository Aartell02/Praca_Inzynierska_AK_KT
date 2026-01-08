using Core;
using GameSystems.AI;
using UnityEngine;

namespace GameSystems
{
    public class EnemyData : MonoBehaviour
    {
		[SerializeField]
		internal EnemyType EnemyType;
		[SerializeField]
		internal int Health;
		[SerializeField]
		internal float MoveSpeed;

		internal Animator Animator;
		internal Rigidbody2D Rigidbody;
		internal SpriteRenderer SpriteRenderer;


		private void Awake()
		{
			this.Animator = this.GetComponent<Animator>();
			this.Rigidbody = this.GetComponent<Rigidbody2D>();
			this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
		}

		public void TakeDamage(int damage)
		{
			this.Health -= damage;
			Animator.SetTrigger("GotHit");
			if (this.Health < 0 )
				Object.Destroy(this.gameObject);
		}
	}
}
