
using BotApp.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.DataAccess
{
    public class BotMenuFieldInputRequestHeaderRepository
    {
        public static List<BotMenuFieldInputRequestHeader> GetListOfHeadersByMenuId(int botMenuId)
        {
            using (AppContext appContext = new AppContext()) {
                    var getLastSession = (
                                          from s in appContext.BotMenuFieldInputRequestHeaders
                                          where s.BotMenuId == botMenuId
                                          select  s
                                      ).ToList();
              return getLastSession;
            }
        }
    }
}
