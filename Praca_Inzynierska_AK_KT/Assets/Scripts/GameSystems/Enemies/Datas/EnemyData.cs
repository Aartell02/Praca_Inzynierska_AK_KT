using Core;
using GameSystems.AI;
using GameSystems.Config;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems
{
    public class EnemyData : MonoBehaviour
    {
		[SerializeField]
		internal EnemyType EnemyType;

		internal int Health { get; private set; }
		internal float MoveSpeed { get; private set; }

		[Header("Combat")]
		[SerializeField]
		internal GameObject AttackHitboxPrefab;

		internal float AttackSpeed { get; private set; }
		internal float ReadyToAttack;

		internal Animator Animator;
		internal Rigidbody2D Rigidbody;
		internal SpriteRenderer SpriteRenderer;

		private StatsConfig StatsConfig;

		private void Awake()
		{
			this.Animator = this.GetComponent<Animator>();
			this.Rigidbody = this.GetComponent<Rigidbody2D>();
			this.SpriteRenderer = this.GetComponent<SpriteRenderer>();

			StatsConfig = ConfigReferences.Instance.statsConfig;
			Health = StatsConfig.EnemyStatsData[(int)EnemyType].Health;
			MoveSpeed = StatsConfig.EnemyStatsData[(int)EnemyType].MovementSpeed;
			AttackSpeed = StatsConfig.EnemyStatsData[(int)EnemyType].AttackSpeed;
		}

		private void Update()
		{
			ReadyToAttack += Time.deltaTime;
			ReadyToAttack = math.min(ReadyToAttack, AttackSpeed);
		}
	}
}
