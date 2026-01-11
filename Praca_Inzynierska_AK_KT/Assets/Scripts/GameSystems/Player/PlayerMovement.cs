using Core;
using Core.Services;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems
{
	public class PlayerMovement : MonoBehaviour
	{
		public float BasicMovementSpeed;
		private float movementSpeed;
		private float currentMoveSpeed;
		private float maxSpeed;
		private float basicMaxSpeed = 5;

		private Rigidbody2D rb;
		private Animator animator;
		private SpriteRenderer spriteRenderer;
		private Camera mainCamera;

		void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
			animator = GetComponentInChildren<Animator>();
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();
			mainCamera = Camera.main;
		}

		void Update()
		{
			maxSpeed = basicMaxSpeed;
			movementSpeed = BasicMovementSpeed;
			if (PlayerInputService.Sprint)
			{
				movementSpeed = BasicMovementSpeed * 2f;
				maxSpeed = basicMaxSpeed * 2f;
			}

			Vector3 mouseScreenPos = PlayerInputService.MousePosition;
			mouseScreenPos.z = Mathf.Abs(mainCamera.transform.position.z);
			Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

			Debug.DrawLine(transform.position, mouseWorldPos, Color.yellow);

			if (spriteRenderer != null)
			{
				spriteRenderer.flipX = mouseWorldPos.x < transform.position.x;
			}

			var inputMove = PlayerInputService.Move;

			bool isMoving = currentMoveSpeed > 0.05f;
			bool isTryingToMove = math.lengthsq(inputMove) > 0f;

			if (isTryingToMove)
			{
				currentMoveSpeed += movementSpeed;
				currentMoveSpeed = math.min(currentMoveSpeed, maxSpeed);
			}
			else if (isMoving)
			{
				currentMoveSpeed -= (movementSpeed);
				currentMoveSpeed = math.max(currentMoveSpeed, 0f);
			}

			float2 lastDir = rb.linearVelocity;

			if (math.lengthsq(lastDir) < 0.0001f) lastDir = new float2(0, 1);

			float2 moveDirection = isTryingToMove ? inputMove : math.normalize(lastDir);

			var moveVector = moveDirection * currentMoveSpeed;
			rb.linearVelocity = new float2(moveVector.x, moveVector.y);

			animator.SetFloat("Move", rb.linearVelocity.magnitude);
		}
	}
}
