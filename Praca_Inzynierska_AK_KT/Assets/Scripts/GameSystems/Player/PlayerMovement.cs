using Core;
using Core.Services;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems
{
	public class PlayerMovement : MonoBehaviour
	{
		private PlayerStats playerStats = PlayerStats.Instance;
		private float movementSpeed;
		private float currentMoveSpeed;

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
			movementSpeed = playerStats.MovementSpeed;
			if (PlayerInputService.Sprint)
			{
				movementSpeed = playerStats.MovementSpeed * Constants.SprintModifier;
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
				currentMoveSpeed += Constants.AccelerationValue;
				currentMoveSpeed = math.min(currentMoveSpeed, movementSpeed);
			}
			else if (isMoving)
			{
				currentMoveSpeed -= Constants.AccelerationValue;
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
