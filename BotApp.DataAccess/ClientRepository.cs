
using BotApp.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.DataAccess
{
    public class ClientRepository
    {
        public static Client GetClient(string code)
        {
            using (AppContext appContext = new AppContext())
            {
                var getLastSession = (
                                      from s in appContext.Clients
                                      where s.Code == code
                                      select s
                                  ).FirstOrDefault();

                return getLastSession;
            }
        } 
    }
}
