using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.Gameplay
{
	[RequireComponent(typeof(Collider2D))]
	public class AltarCapture : MonoBehaviour
	{
		[Header("Settings")]
		[SerializeField] private float captureDuration = 5.0f;
		[SerializeField] private float decaySpeed = 1.0f;
		[SerializeField] private LayerMask playerLayer;

		[Header("Visuals (Optional)")]
		[SerializeField] private SpriteRenderer visualIndicator;
		[SerializeField] private float minAlpha = 0.1f;
		[SerializeField] private Color capturedColor = Color.green;
		[SerializeField] private Color capturingColor = Color.white;

		[Header("Events")]
		public UnityEvent OnCaptured;
		public UnityEvent<float> OnProgress;

		private float currentProgress = 0f;
		private bool isPlayerInside = false;
		private bool isCaptured = false;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (((1 << other.gameObject.layer) & playerLayer) != 0)
			{
				isPlayerInside = true;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (((1 << other.gameObject.layer) & playerLayer) != 0)
			{
				isPlayerInside = false;
			}
		}

		private void Update()
		{
			if (isCaptured) return;

			HandleProgress();
			UpdateVisuals();
		}

		private void HandleProgress()
		{
			if (isPlayerInside)
			{
				currentProgress += Time.deltaTime;

				if (currentProgress >= captureDuration)
				{
					currentProgress = captureDuration;
					CompleteCapture();
				}
			}
			else if (currentProgress > 0)
			{
				currentProgress -= Time.deltaTime * decaySpeed;
				currentProgress = Mathf.Max(currentProgress, 0f);
			}

		}

		private void UpdateVisuals()
		{
			if (visualIndicator == null) return;

			if (isCaptured)
			{
				Color finalColor = capturedColor;
				finalColor.a = 1f;
				visualIndicator.color = finalColor;
			}
			else
			{
				float t = currentProgress / captureDuration;
				Color displayColor = capturingColor;
				displayColor.a = Mathf.Lerp(minAlpha, 1f, t);
				visualIndicator.color = displayColor;
			}
		}

		private void CompleteCapture()
		{
			isCaptured = true;
			Debug.Log("Ołtarz przejęty!");
		}
	}
}
