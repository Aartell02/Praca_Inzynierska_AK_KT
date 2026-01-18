using Core;
using GameSystems.AI;
using UnityEngine;

namespace GameSystems
{
	public class LifeStateData : MonoBehaviour
	{
		internal int Health;

		internal Animator Animator;

		private void Awake()
		{
			this.Animator = this.GetComponent<Animator>();
			if (this.GetComponent<PlayerState>())
			{
				Health = this.GetComponent<PlayerState>().playerStats.Health;
			}
			else if (this.GetComponent<EnemyData>())
			{
				Health = this.GetComponent<EnemyData>().Health;
			}
		}

		public void TakeDamage(int damage, GameObject attacker)
		{
			this.Health -= damage;
			Animator.SetTrigger("GotHit");
			if (this.Health < 0)
			{
				RewardAttacker(attacker);
				Object.Destroy(this.gameObject);
			}
		}

		private void RewardAttacker(GameObject attacker)
		{
			if (attacker.GetComponent<PlayerState>())
			{
				var playerStats = attacker.GetComponent<PlayerState>();
				playerStats.Experience += 30;
			}
		}
	}
}
