using Core;
using GameSystems.AI;
using UnityEngine;

namespace GameSystems
{
	public class LifeStateData : MonoBehaviour
	{
		[SerializeField]
		internal int Health;

		internal Animator Animator;

		private void Awake()
		{
			this.Animator = this.GetComponent<Animator>();
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
