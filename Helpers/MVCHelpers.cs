using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Helpers
{
	public static class MVCHelpers
	{
		public static List<string> GetActionsForController(this Assembly assembly, string controllerName,
			params string[] attributes)
		{
			Type controller = GetControllers(assembly, controllerName).FirstOrDefault();
			List<string> actions = GetActions(controller, attributes);
			return actions;

		}

		public static List<Type> GetControllers(Assembly assembly, string name = "")
		{
			List<Type> controllers = assembly
				.GetTypes()
				.Where(
					type => type.IsSubclassOf(typeof (Controller))
					        && (
						        string.IsNullOrWhiteSpace(name)
						        || type.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
						        )
				)
				.ToList();
			return controllers;
		}

		public static List<String> GetActions(Type controller)
		{
			return GetActions(controller, null);
		}

		public static List<String> GetActions(Type controller, string[] attributesToFilterOn)
		{
			// List of links
			var items = new List<String>();

			// Get a descriptor of this controller
			var controllerDesc = new ReflectedControllerDescriptor(controller);

			// Look at each action in the controller
			foreach (ActionDescriptor action in controllerDesc.GetCanonicalActions())
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

				// Add the action to the list if it's "valid"
				items.Add(action.ActionName);
			}
			return items;
		}

		private static string RenderViewToString(ControllerContext context,
			string viewPath,
			object model = null,
			bool partial = false)
		{
			// first find the ViewEngine for this view
			ViewEngineResult viewEngineResult = null;
			if (partial)
				viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
			else
				viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

			if (viewEngineResult == null)
				throw new FileNotFoundException("View cannot be found.");

			// get the view and attach the model to view data
			var view = viewEngineResult.View;
			context.Controller.ViewData.Model = model;

			string result = null;

			using (var sw = new StringWriter())
			{
				var ctx = new ViewContext(context, view,
					context.Controller.ViewData,
					context.Controller.TempData,
					sw);
				view.Render(ctx, sw);
				result = sw.ToString();
			}

			return result;
		}
	}


}
