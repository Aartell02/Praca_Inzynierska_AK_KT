using UnityEngine;

namespace Gameplay.Controllers
{
	public class WorldSceneController : MonoBehaviour
	{
		private void Awake()
		{
			GameplayViewModel.GenerateMap();
		}
	}
}
