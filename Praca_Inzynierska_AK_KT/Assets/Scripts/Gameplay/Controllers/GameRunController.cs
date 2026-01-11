using Boot;
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
		}
	}
}
