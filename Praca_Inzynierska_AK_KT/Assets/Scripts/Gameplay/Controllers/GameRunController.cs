using Boot;
using Core.Services;
using UnityEngine;

namespace Gameplay.Controllers
{
	public class GameRunController : MonoBehaviour
	{
		[SerializeField] int floorCount;

		private void Start()
		{
			GameRunState.SetFloorCount(floorCount);
			GameRunState.LoadNextFloor();
			PlayerInputService._inputActions.Player.Enable();
			PlayerInputService._inputActions.UI.Disable();
		}

		private void OnDestroy()
		{
			PlayerInputService._inputActions.Player.Disable();
			PlayerInputService._inputActions.UI.Enable();
		}
	}
}
