using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Helpers;
using WebApplicationData;
using WebApplicationData.Comic;

namespace WebApplicationBusiness
{
	public class ComicBusiness
	{
		public void AddItemToCache(string key, object cachItem)
		{
			TimeSpan result = (DateTimeHelpers.GetTonightMidnight() - DateTime.Now);
			double timeToExpire = result.TotalMinutes;
			Cache.Add(key, key, timeToExpire);
		}
		public T GetItemFromCache<T>(string key) where T : class
		{
			if (!Cache.Exists(key))
			{
				return null;
			}
			T t = Cache.Get<T>(key);
			return t;
		}

		public T GetComicModelFromCache<T>(string cacheKey = null) where T : class
		{
			string key = string.IsNullOrWhiteSpace(cacheKey) ? typeof(T).Name : cacheKey;
			if (!Cache.Exists(key))
			{
				return null;
			}
			T t = Cache.Get<T>(key);
			return t;
		}

		public void AddComicToCache<T>(T t, string givenKey = null) where T : class
		{
			string key = string.IsNullOrWhiteSpace(givenKey) ? typeof(T).Name : givenKey;
			TimeSpan result = (DateTimeHelpers.GetTonightMidnight() - DateTime.Now);
			double timeToExpire = result.TotalMinutes;
			Cache.Add(t, key, timeToExpire);
		}

		public List<ComicInfo> GetComicInfosForController(Assembly assembly, string controllerName,
			params string[] attributes)
		{
			Type controller = GetControllers(assembly, controllerName).FirstOrDefault();
			List<ComicInfo> actions = GetActions(assembly, controller, attributes);
			return actions;

		}

		public static List<Type> GetControllers(Assembly assembly, string name = "")
		{
			List<Type> controllers = assembly
				.GetTypes()
				.Where(
					type => type.IsSubclassOf(typeof(Controller))
							&& (
								string.IsNullOrWhiteSpace(name)
								|| type.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
								)
				)
				.ToList();
			return controllers;
		}

		List<string> authorizeAttributes;

		public ComicBusiness(List<string> attributes = null)
		{
			this.authorizeAttributes = attributes;
		}


		public List<ComicInfo> GetActions(Assembly assembly, Type controller, string[] attributesToFilterOn)
		{
			// List of links
			var items = new List<ComicInfo>();

			// Get a descriptor of this controller
			var controllerDesc = new ReflectedControllerDescriptor(controller);

			ActionDescriptor[] actions = controllerDesc.GetCanonicalActions();

			// Look at each action in the controller
			foreach (ActionDescriptor action in actions)
			{
				// Get any attributes (filters) on the action
				object[] attributes = action.GetCustomAttributes(false);

				// Look at each attribute
				bool validAction = attributes.All(filter => !(filter is HttpPostAttribute)
															&& !(filter is ChildActionOnlyAttribute));

				if (!validAction) continue;

				if (attributesToFilterOn != null)
				{
					bool valid = false;
					foreach (string s in attributesToFilterOn)
					{
						valid = attributes.Any(o => o.ToString().Equals(s));
						if (!valid) break;

					}
					if (!valid) continue;
				}

				string modelName = string.Format("WebApplication1.Models.Comic.{0}Model", action.ActionName);
				object x = assembly.CreateInstance(modelName);

				ComicInfo ci = new ComicInfo();
				ci.Name = action.ActionName;
				ci.Model = x;

				if (authorizeAttributes == null || !authorizeAttributes.Any())
				{
					return items;
				}


				object authAttribute = attributes.FirstOrDefault(o => authorizeAttributes.Any(a => a.Equals(o.ToString(), StringComparison.InvariantCultureIgnoreCase)));
				if (authAttribute != null)
				{
					PropertyInfo property = authAttribute.GetType().GetProperty("AllowedRoles");
					//string value = property.GetValue(authAttribute).ToString();
					//List<string> roles = value.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries).ToList();
					List<string> roles = property.GetValue(authAttribute) as List<string>;
					ci.Roles = roles;
				}


				// Add the action to the list if it's "valid"
				items.Add(ci);
			}
			return items;
		}
	}
}
