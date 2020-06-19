using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Extensions
{
	public static partial  class Extensions
	{

		/// <summary>
		///将字符串转换为枚举值
		/// </summary>
		/// <typeparam name="T">枚举类型</typeparam>
		/// <param name="value">要转换的字符串值</param>
		/// <returns>返回枚举的值</returns>
		public static T ToEnum<T>(this string value)
			where T : struct
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			return (T)Enum.Parse(typeof(T), value);
		}


		/// <summary>
		/// 将字符串转换为枚举值。
		/// </summary>
		/// <typeparam name="T">枚举类型</typeparam>
		/// <param name="value">要转换的字符串值</param>
		/// <param name="ignoreCase">是否忽略大小写</param>
		/// <returns>返回枚举的值</returns>
		public static T ToEnum<T>(this string value, bool ignoreCase)
			where T : struct
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			return (T)Enum.Parse(typeof(T), value, ignoreCase);
		}
	}
}
