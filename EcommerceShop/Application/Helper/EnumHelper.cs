using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Helper
{
	public static class EnumHelper
	{
		public static Dictionary<int, string> GetList(Type type)
		{
			var dict = new Dictionary<int, string>();
			foreach (var name in Enum.GetNames(type))
			{
				dict.Add((int)Enum.Parse(type, name), name);
			}
			return dict;
		}
	}
}
