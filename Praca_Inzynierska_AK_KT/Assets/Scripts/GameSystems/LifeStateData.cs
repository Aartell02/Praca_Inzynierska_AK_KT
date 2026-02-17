using UnityEngine;

namespace GameSystems
{
	public class LifeStateData : MonoBehaviour
	{
		internal int Health;

		internal Animator Animator;

		float Timer = 0;
		private void Start()
		{
			this.Animator = this.GetComponent<Animator>();
			if (this.GetComponent<PlayerState>())
			{
				Health = PlayerStats.Instance.Health;
			}
			else if (this.GetComponent<EnemyData>())
			{
				Health = this.GetComponent<EnemyData>().Health;
			}
		}

		private void Update()
		{
			if (this.GetComponent<PlayerState>())
			{
				Timer += Time.deltaTime;
				if (Timer > 0.5f)
				{
					Health = Mathf.Min(PlayerStats.Instance.Health, Health+2);
					Timer -= 0.5f;
				}
			}
		}

		public void TakeDamage(int damage, GameObject attacker)
		{
			this.Health -= damage;
			Animator.SetTrigger("GotHit");
			if (this.Health < 0)
			{
				Debug.Log($"{this.gameObject} got killed by {attacker}");
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
