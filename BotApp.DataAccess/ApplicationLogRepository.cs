
using BotApp.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.DataAccess
{
    public class ApplicationLogRepository
    {
        public static void LogRequest(string description)
        {
            using (BotApp.DataAccess.AppContext appContext = new BotApp.DataAccess.AppContext())
            {
                appContext.ApplicationLogs.Add(new ApplicationLog() { Description = description, CreatedDate = DateTime.Now });
                appContext.SaveChanges();
            }
        }
    }
}
