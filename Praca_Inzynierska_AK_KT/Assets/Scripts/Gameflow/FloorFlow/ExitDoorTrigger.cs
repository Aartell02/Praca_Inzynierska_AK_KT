using Boot;
using UnityEngine;

namespace Gameplay
{
	public class ExitDoortrigger : MonoBehaviour
	{
		BoxCollider2D Collider;

		void Start()
		{
			Collider = GetComponent<BoxCollider2D>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.CompareTag("Player"))
				return;

			Debug.Log("Gracz wszedł w trigger drzwi wyjściowych");
			GameRunState.LoadNextFloor();
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (!other.CompareTag("Player"))
				return;

			Debug.Log("Gracz opuścił trigger drzwi wyjściowych");
		}
	}
}
