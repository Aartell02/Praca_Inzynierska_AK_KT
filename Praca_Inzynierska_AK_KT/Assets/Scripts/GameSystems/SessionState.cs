using Core;
using GameSystems.AI;
using UnityEngine;

namespace GameSystems
{
	public class SessionState : MonoBehaviour
	{
		internal int Health;

		internal Animator Animator;

		private void Awake()
		{
			this.Animator = this.GetComponent<Animator>();
			if (this.GetComponent<PlayerStats>())
			{
				Health = this.GetComponent<PlayerStats>().Health;
			}
			else if (this.GetComponent<EnemyData>())
			{
				Health = this.GetComponent<EnemyData>().Health;
			}
		}

		public void TakeDamage(int damage)
		{
			this.Health -= damage;
			Animator.SetTrigger("GotHit");
			if (this.Health < 0)
				Object.Destroy(this.gameObject);
		}
	}
}
