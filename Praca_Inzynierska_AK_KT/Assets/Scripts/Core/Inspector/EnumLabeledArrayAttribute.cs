using System;
using UnityEngine;

namespace Core.Inspector
{
	[AttributeUsage(AttributeTargets.Field)]
	public class EnumLabeledArrayAttribute : PropertyAttribute
	{
		public Type EnumType { get; private set; }

		public EnumLabeledArrayAttribute(Type enumType)
		{
			if (!enumType.IsEnum)
				throw new ArgumentException("EnumLabeledArrayAttribute: Type must be an enum!");

			EnumType = enumType;
		}
	}
}
