using UnityEngine;

namespace Core
{
	[CreateAssetMenu(fileName = "RandomWalkParameters_", menuName = "PCG/RandomWalkData")]

	public class RandomWalkData : ScriptableObject
    {
		public int iterations = 150;
		public int walkLength = 200;
		public bool startRandomlyEachIteration = true;
    }
}
