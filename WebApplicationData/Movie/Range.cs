using System.Web.Helpers;

namespace WebApplicationData.Movie
{
	public class Range
	{
		public override string ToString()
		{
			return Json.Encode(this);
		}

		public string AsString()
		{
			return string.Format("[ {0} - {1} ]", this.Min, this.Max);
		}
		public int Min { get; set; }
		public int Max { get; set; }
	}
}