using System;
using UnityEngine;

namespace Core.Inspector
{
	[AttributeUsage(AttributeTargets.Field)]
	public class EnumLabelsAttribute : PropertyAttribute
	{
		public Type EnumType { get; private set; }

		public EnumLabelsAttribute(Type enumType)
		{
			if (!enumType.IsEnum)
				throw new ArgumentException("EnumLabelsAttribute: Provided type is not an enum");
			EnumType = enumType;
		}
	}
}
