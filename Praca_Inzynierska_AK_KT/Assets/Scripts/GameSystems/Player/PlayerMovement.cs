using Core;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems
{
    public class PlayerMovement : MonoBehaviour
    {
		public float MovementSpeed;
		private float currentMoveSpeed;
		private const float maxSpeed = 5;
		private bool lastFlipX = false;

		private Rigidbody2D rb;
		private Animator animator;
		private SpriteRenderer spriteRenderer;

		void Awake()
        {
			rb = GetComponent<Rigidbody2D>();
			animator = GetComponentInChildren<Animator>();
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		}

        void Update()
        {
			var inputMove = CoreViewModel.Move;

			bool isMoving = currentMoveSpeed > 0.05f;
			bool isTryingToMove = math.lengthsq(inputMove) > 0f;

			if (isTryingToMove)
			{
				lastFlipX = inputMove.x < 0f;
				spriteRenderer.flipX = lastFlipX;
				currentMoveSpeed += MovementSpeed;
				currentMoveSpeed = math.min(currentMoveSpeed, maxSpeed);
			}
			else if (isMoving)
			{
				spriteRenderer.flipX = lastFlipX;
				currentMoveSpeed -= (MovementSpeed);
				currentMoveSpeed = math.max(currentMoveSpeed, 0f);
			}

			float2 lastDir = rb.linearVelocity;
			if (math.lengthsq(lastDir) < 0.0001f) lastDir = new float2(0, 1);
			float2 moveDirection = isTryingToMove ? inputMove : math.normalize(lastDir);


			var moveVector = moveDirection * currentMoveSpeed;
			rb.linearVelocity = new float2(moveVector.x, moveVector.y);

			Debug.Log(rb.linearVelocity.magnitude);
			animator.SetFloat("Move", rb.linearVelocity.magnitude);
		}
    }
}
