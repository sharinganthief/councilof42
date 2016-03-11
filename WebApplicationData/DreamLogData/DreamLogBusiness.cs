using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WebApplicationData.DreamLogData
{
	public class DreamLogBusiness
	{
		public List<DreamLog> GetLogs()
		{
			using (WebApplication1Entities entities = new WebApplication1Entities())
			{
				return entities.DreamLogs.ToList();
			}
		}

		public DreamLog GetLog(Guid dreamLogID)
		{
			using (WebApplication1Entities entities = new WebApplication1Entities())
			{
				return entities.DreamLogs.FirstOrDefault(o => o.Id == dreamLogID);
			}
		}

		public void AddEditLog(DreamLog logToAddEdit)
		{
			using (WebApplication1Entities entities = new WebApplication1Entities())
			{
				DreamLog log = entities.DreamLogs.FirstOrDefault(o => o.Id == logToAddEdit.Id);

				bool logNull = log == null;

				if (logNull)
				{
					log = new DreamLog();
				}

				log.Id = logToAddEdit.Id;
				log.Text = logToAddEdit.Text;
				log.Title = logToAddEdit.Title;
				log.Date = DateTime.Now;


				if (logNull)
				{
					entities.DreamLogs.Add(log);
				}

				entities.SaveChanges();

			}
		}
	}
}
