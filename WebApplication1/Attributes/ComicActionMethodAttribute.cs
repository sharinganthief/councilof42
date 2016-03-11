using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Attributes
{
	[System.AttributeUsage(System.AttributeTargets.Method)]
	public class ComicActionMethodAttribute : Attribute
	{
	}
}