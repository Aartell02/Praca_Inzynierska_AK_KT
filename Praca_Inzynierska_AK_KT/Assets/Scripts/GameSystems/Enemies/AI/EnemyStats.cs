using Core;
using UnityEngine;

namespace GameSystems
{
    class EnemyStats : MonoBehaviour
    {
		[SerializeField]
		internal EnemyType EnemyType;
		[SerializeField]
		internal int Heatlh;
		[SerializeField]
		internal float MoveSpeed;
    }
}
