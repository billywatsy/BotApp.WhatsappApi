
using BotApp.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.DataAccess
{
    public class BotFieldInputRepository
    {
        public static List<BotFieldInput> GetFieldsByMenuId(int botMenuId)
        {
            using (AppContext appContext = new AppContext()) {
                    var getLastSession = (
                                          from s in appContext.BotFieldInputs
                                          where s.BotMenuId == botMenuId
                                          select  s
                                      ).ToList();
              return getLastSession;
            }
        }
        public static BotFieldInput GetField(int botFieldInputId)
        {
            using (AppContext appContext = new AppContext())
            {
                var getLastSession = (
                                      from s in appContext.BotFieldInputs
                                      where s.BotFieldInputId == botFieldInputId
                                      select s
                                    ).FirstOrDefault();
                return getLastSession;

            }
        }
    }
}
