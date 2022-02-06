
using BotApp.Domain.Entity;
using BotApp.Domain.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.DataAccess
{
    public class BotSessionRepository
    {
        public static BotSessionDto LastActiveMessage(string wa_id)
        {
            using (AppContext appContext = new AppContext())
            {
                var getLastSession = (
                                      from s in appContext.WhatsAppPhones
                                      join t in appContext.BotSessions on s.WhatsAppPhoneId equals t.WhatsAppPhoneId
                                      where s.Wa_id == wa_id
                                      && t.IsValid == true
                                      select new BotSessionDto()
                                      {
                                          BotSessionId = t.BotSessionId,
                                          BotMenuId = t.BotMenuId,
                                          WhatsAppPhoneId = t.WhatsAppPhoneId,
                                          IsValid = t.IsValid,
                                          BotFieldInputId = t.BotFieldInputId,
                                          BotFieldSubInputId = t.BotFieldSubInputId,
                                              //JSON OBJECT
                                              DynamicFormFieldJsonObject = t.DynamicFormFieldJsonObject,
                                          Wa_id = s.Wa_id,
                                          UserScreenLookUpDataTemplate = t.UserScreenLookUpDataTemplate,
                                          UserScreenText = t.UserScreenText,
                                          ReplaceableFieldCode = t.ReplaceableFieldCode,
                                          LastDate = t.CreatedDate
                                      }
                                  )
                                  .OrderByDescending(p => p.LastDate)
                                  .FirstOrDefault();
                return getLastSession;

            }
        }

        private static bool LastActiveMessage(int whatsAppPhoneId, string replaceableFieldCode)
        {
            using (AppContext appContext = new AppContext())
            {
                var getLastSession = (
                                      from s in appContext.WhatsAppPhones
                                      join t in appContext.BotSessions on s.WhatsAppPhoneId equals t.WhatsAppPhoneId
                                      where s.WhatsAppPhoneId == whatsAppPhoneId
                                      && t.IsValid == true
                                      select t)
                                  .OrderByDescending(p => p.CreatedDate)
                                  .FirstOrDefault();

                if(getLastSession != null && !string.IsNullOrEmpty(replaceableFieldCode))
                {
                    getLastSession.ReplaceableFieldCode = replaceableFieldCode;
                    appContext.Update(getLastSession);
                    appContext.SaveChanges();
                }
                return true;

            }
        }


        public static bool SetResponseWhenValidForMenuFinalForms(int botMenuId, Newtonsoft.Json.Linq.JObject formField , string userScreenText, int whatsAppPhoneId )
        {
            using (AppContext appContext = new AppContext())
            {
                var botSession = new BotSession();
                botSession.BotMenuId = botMenuId;
                botSession.WhatsAppPhoneId = whatsAppPhoneId;
                botSession.UserScreenText = userScreenText;
                botSession.DynamicFormFieldJsonObject = formField?.ToString() ?? "";
                botSession.IsValid = true;
                botSession.ModifiedDate = DateTime.Now;
                botSession.CreatedDate = DateTime.Now;
                botSession.ModifiedBy = "system";
                botSession.CreatedBy = "system";

                appContext.BotSessions.Add(botSession);

                appContext.SaveChanges();
            }
            return true;
        }

        public static bool SetResponseWhenValidForMenu(int botMenuId, string userScreenText, int whatsAppPhoneId )
        {
            using (AppContext appContext = new AppContext())
            {
                var botSession = new BotSession();
                botSession.BotMenuId = botMenuId;
                botSession.WhatsAppPhoneId = whatsAppPhoneId;
                botSession.UserScreenText = userScreenText;
                botSession.IsValid = true;
                botSession.ModifiedDate = DateTime.Now;
                botSession.CreatedDate = DateTime.Now;
                botSession.ModifiedBy = "system";
                botSession.CreatedBy = "system";

                appContext.BotSessions.Add(botSession);

                appContext.SaveChanges();
            }
            return true;
        }

        public static bool SetResponseWhenValidForField(int botFieldInputId, string userScreenText, Newtonsoft.Json.Linq.JObject formField , int whatsAppPhoneId , string replaceableFieldCode)
        {
            using (AppContext appContext = new AppContext())
            {
                var botSession = new BotSession();
                botSession.BotFieldInputId = botFieldInputId;
                botSession.WhatsAppPhoneId = whatsAppPhoneId;
                botSession.UserScreenText = userScreenText;
                botSession.ReplaceableFieldCode = replaceableFieldCode;
                botSession.DynamicFormFieldJsonObject = formField?.ToString() ?? "";
                botSession.IsValid = true;
                botSession.ModifiedDate = DateTime.Now;
                botSession.CreatedDate = DateTime.Now;
                botSession.ModifiedBy = "system";
                botSession.CreatedBy = "system";

                appContext.BotSessions.Add(botSession);

                appContext.SaveChanges();
            }
            return true;
        }
        public static bool SetResponseWhenValidForFieldList(int botFieldInputId, string userScreenText, Newtonsoft.Json.Linq.JObject formField , Newtonsoft.Json.Linq.JArray userScreenLookUpDataTemplate, int whatsAppPhoneId, string replaceableFieldCode )
        {
            using (AppContext appContext = new AppContext())
            {
                var botSession = new BotSession();
                botSession.BotFieldInputId = botFieldInputId;
                botSession.WhatsAppPhoneId = whatsAppPhoneId;
                botSession.UserScreenText = userScreenText;
                botSession.ReplaceableFieldCode = replaceableFieldCode;
                botSession.DynamicFormFieldJsonObject = formField?.ToString() ?? "";
                botSession.UserScreenLookUpDataTemplate = userScreenLookUpDataTemplate?.ToString() ?? "";
                botSession.IsValid = true;
                botSession.ModifiedDate = DateTime.Now;
                botSession.CreatedDate = DateTime.Now;
                botSession.ModifiedBy = "system";
                botSession.CreatedBy = "system";

                appContext.BotSessions.Add(botSession);

                appContext.SaveChanges();
            }
            return true;
        }


    }
}
