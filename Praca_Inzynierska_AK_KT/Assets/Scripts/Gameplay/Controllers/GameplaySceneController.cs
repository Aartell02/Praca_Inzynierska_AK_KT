using Core;
using DOTS;
using System.Collections;
using Unity.Scenes;
using UnityEngine;

namespace Gameplay.Controllers
{
	public class GameplaySceneController : MonoBehaviour
	{
		public SubScene SubScene;

		private void Awake()
		{
			StartCoroutine(WaitForSubSceneAndSpawn());
		}

		private IEnumerator WaitForSubSceneAndSpawn()
		{
			if (SubScene == null)
			{
				Debug.LogWarning("Brak przypisanej SubScene!");
				yield break;
			}

		}
	}
}
