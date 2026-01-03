using Core;
using UnityEngine;

namespace GameSystems
{
    public class EnemyData : MonoBehaviour
    {
		[SerializeField]
		internal EnemyType EnemyType;
		[SerializeField]
		internal int Heatlh;
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
	}
}
