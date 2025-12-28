using Core.Inspector;
using UnityEngine;

namespace GameSystems
{
	class PlayerAttack : MonoBehaviour
	{
		[SerializeField]
		internal int AttackDamage;

		[SerializeField]
		[EnumArray(typeof(AttackType))]
		internal float[] DamageScaling;

	}
}
