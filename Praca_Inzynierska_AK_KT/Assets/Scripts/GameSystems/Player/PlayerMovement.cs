using Core;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Windows;

namespace DOTS
{
    public class PlayerMovement : MonoBehaviour
    {
		public float MovementSpeed;
		private float currentMoveSpeed;
		private const float maxSpeed = 10;
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
			var mousePosition = CoreViewModel.MousePosition;
			var inputMove = CoreViewModel.Move;

			bool isMoving = currentMoveSpeed > 0.05f;
			bool isTryingToMove = math.lengthsq(inputMove) > 0f;

			if (isTryingToMove)
			{
				currentMoveSpeed += MovementSpeed;
				currentMoveSpeed = math.min(currentMoveSpeed, maxSpeed);
			}
			else if (isMoving)
			{
				currentMoveSpeed -= (MovementSpeed);
				currentMoveSpeed = math.max(currentMoveSpeed, 0f);
			}


			if (mousePosition.x < rb.position.x)
				spriteRenderer.flipX = true;
			else
				spriteRenderer.flipX = false;

			float2 lastDir = rb.linearVelocity;
			if (math.lengthsq(lastDir) < 0.0001f) lastDir = new float2(0, 1);
			float2 moveDirection = isTryingToMove ? inputMove : math.normalize(lastDir);


			var moveVector = moveDirection * currentMoveSpeed;
			rb.linearVelocity = new float2(moveVector.x, moveVector.y);

			animator.SetFloat("Move", rb.linearVelocity.magnitude);
		}
    }
}
