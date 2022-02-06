
using BotApp.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.DataAccess
{
    public class BotFieldValidationTypeRepository
    {
        public static BotFieldValidationType GetBotFieldValidationTypeId(int botFieldValidationTypeId)
        {
            using (AppContext appContext = new AppContext()) {
                    var getLastSession = (
                                          from s in appContext.BotFieldValidationTypes
                                          where s.BotFieldValidationTypeId == botFieldValidationTypeId
                                          select  s
                                      ).FirstOrDefault();
              return getLastSession;
            }
        } 
    }
}
