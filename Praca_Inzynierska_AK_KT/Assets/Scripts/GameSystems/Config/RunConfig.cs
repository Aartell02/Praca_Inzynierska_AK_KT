using UnityEngine;

namespace GameSystems.Config
{
    [CreateAssetMenu(fileName = "RunConfig", menuName = "Config/RunConfig")]
    public class RunConfig : ScriptableObject
    {
		[SerializeField] public int floorCount;
	}
}
