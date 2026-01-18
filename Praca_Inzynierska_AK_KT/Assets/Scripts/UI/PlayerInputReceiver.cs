using Core;
using Core.Services;
using UnityEngine;

namespace GameSystems
{
	class PlayerInputReceiver : MonoBehaviour
	{

		internal void Update()
		{
			if (PlayerInputService.Pause)
			{
				FindFirstObjectByType<PauseMenu>().PauseGame();
			}
			if (PlayerInputService.Cancel)
			{
				FindFirstObjectByType<PauseMenu>().ResumeGame();
			}
		}
	}
}
