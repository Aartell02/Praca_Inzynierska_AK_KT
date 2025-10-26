using UnityEngine;

namespace DOTS
{
	[CreateAssetMenu(fileName = "RandomWalkParameters_", menuName = "PCG/RandomWalkData")]

	public class RandomWalkData : ScriptableObject
    {
		public int iterations = 150, walkLength = 200;
		public bool startRandomlyEachIteration = true;
    }
}
