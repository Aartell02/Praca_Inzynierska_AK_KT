using UnityEngine;
using UnityEngine.UIElements;

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
