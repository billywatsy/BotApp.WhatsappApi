
using BotApp.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.DataAccess
{
    public class BotFieldInputTypeRepository
    {
        public static BotFieldInputType GetBotFieldInputTypeById(int botFieldInputTypeId)
        {
            using (AppContext appContext = new AppContext()) {
                    var getLastSession = (
                                          from s in appContext.BotFieldInputTypes
                                          where s.BotFieldInputTypeId == botFieldInputTypeId
                                          select  s
                                      ).FirstOrDefault();
              return getLastSession;
            }
        } 
        public static BotFieldInputType GetBotFieldInputTypeByCode(string botFieldInputTypeCode)
        {
            using (AppContext appContext = new AppContext()) {
                    var getLastSession = (
                                          from s in appContext.BotFieldInputTypes
                                          where s.Code == botFieldInputTypeCode
                                          select  s
                                      ).FirstOrDefault();
              return getLastSession;
            }
        } 
        public static List<BotFieldInputType> GetList()
        {
            using (AppContext appContext = new AppContext()) {
                    var getLastSession = (
                                          from s in appContext.BotFieldInputTypes 
                                          select  s
                                      ).ToList();
              return getLastSession;
            }
        } 
    }
}
