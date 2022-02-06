
using BotApp.Domain.Entity;
using BotApp.Domain.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.DataAccess
{
    public class WhatsAppPhoneRepository
    {
        public static BotSessionDto GetUserWithNoSession(string wa_id)
        {
            using (AppContext appContext = new AppContext())
            {
                var getLastSession = (
                                      from s in appContext.WhatsAppPhones
                                      where s.Wa_id == wa_id
                                      select new BotSessionDto()
                                      {
                                          WhatsAppPhoneId = s.WhatsAppPhoneId,
                                          IsValid = true,
                                              //JSON OBJECT
                                          DynamicFormFieldJsonObject = "",
                                          Wa_id = s.Wa_id,
                                          UserScreenText = "0",
                                          LastDate = DateTime.Now
                                      }
                                  ).FirstOrDefault();
                return getLastSession;

            }
        }

        public static WhatsAppPhone SaveNew(string wa_id)
        {
            using (AppContext appContext = new AppContext())
            {
                var getLastSession = (
                                      from s in appContext.WhatsAppPhones
                                      where s.Wa_id == wa_id
                                      select s
                                  ).FirstOrDefault();
                
                if(getLastSession == null)
                {
                    appContext.WhatsAppPhones.Add(new WhatsAppPhone()
                    {
                        Wa_id = wa_id,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = "system",
                        CreatedBy = "system"
                    });

                    appContext.SaveChanges();

                    getLastSession = (
                                          from s in appContext.WhatsAppPhones
                                          where s.Wa_id == wa_id
                                          select s
                                      ).FirstOrDefault();

                    return getLastSession;
                }
                return getLastSession;
            }
        }

    }
}
