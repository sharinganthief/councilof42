using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
	public static class DateTimeHelpers
	{
		public static DateTime GetTonightMidnight()
		{
			//return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);
			var tomorrow = DateTime.Now.AddDays(1).Date;
			return new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 0, 0, 0);
		}
	}
}
