using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
	public static class ReflectionHelpers
	{
		public static void TrySetProperty(object obj, string property, object value)
		{
			PropertyInfo prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
			if (prop != null && prop.CanWrite)
				prop.SetValue(obj, value, null);
		}

		public static void SetPropertyValues<T>(T obj1, T obj2) where T : class
		{
			SetPropertyValues<T, T>(obj1, obj2);
		}


		public static void SetPropertyValues<T,TK>(T obj1, TK obj2) where T : class where TK : class 
		{
			Type object1Type = obj1.GetType();
			Type object2Type = obj2.GetType();

			PropertyInfo[] info1 = object1Type.GetProperties();
			PropertyInfo[] info2 = object2Type.GetProperties();
			foreach (PropertyInfo propertyInfo in info1)
			{
				if (!info2.Any(o => o.Name.Equals(propertyInfo.Name) && o.PropertyType == propertyInfo.PropertyType)) continue;

				var propValue = propertyInfo.GetValue(obj2);

				propertyInfo.SetValue(obj1, propValue);

			}
		}
	}
}
