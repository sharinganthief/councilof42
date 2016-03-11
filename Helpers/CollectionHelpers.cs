using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
	public static class CollectionHelpers
	{
		public static List<TSource> GetPaginated<TSource>(this List<TSource> collection, int page, int recordsPerPage)
		{
			int skipRecords = page * recordsPerPage;
			
			return collection.Skip(skipRecords).Take(recordsPerPage).ToList();
		}
	}
}
