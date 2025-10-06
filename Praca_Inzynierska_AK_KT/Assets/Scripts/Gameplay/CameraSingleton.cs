using UnityEngine;

namespace DOTS.Authoring
{
	[DisallowMultipleComponent]
	public class CameraSingleton : MonoBehaviour
	{
		public static CameraSingleton Instance;

		public void Awake()
		{
			Instance = this;
		}

	}
}
