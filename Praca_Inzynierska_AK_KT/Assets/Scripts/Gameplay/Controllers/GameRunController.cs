using Boot;
using Core.Services;
using GameSystems;
using GameSystems.Config;
using UnityEngine;

namespace Gameplay.Controllers
{
	public class GameRunController : MonoBehaviour
	{
		RunConfig runConfig = ConfigReferences.Instance.runConfig;
		private void Start()
		{
			GameRunState.SetFloorCount(runConfig.floorCount);
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
