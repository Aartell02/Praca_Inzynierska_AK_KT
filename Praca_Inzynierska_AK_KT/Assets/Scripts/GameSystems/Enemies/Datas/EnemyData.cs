using Core;
using GameSystems.AI;
using Unity.Mathematics;
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

		[Header("Combat")]
		[SerializeField]
		internal GameObject AttackHitboxPrefab;

		[SerializeField]
		internal float AttackDelay = 1f;
		internal float ReadyToAttack;

		internal Animator Animator;
		internal Rigidbody2D Rigidbody;
		internal SpriteRenderer SpriteRenderer;


		private void Awake()
		{
			this.Animator = this.GetComponent<Animator>();
			this.Rigidbody = this.GetComponent<Rigidbody2D>();
			this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			ReadyToAttack += Time.deltaTime;
			ReadyToAttack = math.min(ReadyToAttack, AttackDelay);
		}
	}
}
