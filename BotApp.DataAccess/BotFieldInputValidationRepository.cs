
using BotApp.Domain.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.DataAccess
{
    public class BotFieldInputValidationRepository
    {
        public static List<BotFieldInputValidationDto> GetListByFieldId(int botFieldInputId)
        {
            using (AppContext appContext = new AppContext()) {
                    var getLastSession = (
                                          from s in appContext.BotFieldInputValidations
                                          join q in appContext.BotFieldInputs on s.BotFieldInputId equals q.BotFieldInputId
                                          join t in appContext.BotFieldValidationTypes on s.BotFieldValidationTypeId equals t.BotFieldValidationTypeId
                                          where s.BotFieldInputId == botFieldInputId
                                          select  new BotFieldInputValidationDto
                                          {
                                              BotFieldValidationTypeCode = t.Code ,
                                              Value =  s.Value
                                          }
                                      ).ToList();
              return getLastSession;
            }
        } 
    }
}
