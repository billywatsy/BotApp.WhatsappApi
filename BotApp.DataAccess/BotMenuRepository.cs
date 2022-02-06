
using BotApp.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.DataAccess
{
    public class BotMenuRepository
    {
        public static List<BotMenu> GetMenuDefaultHome()
        {
            using (AppContext appContext = new AppContext()) {
                    var getLastSession = (
                                          from s in appContext.BotMenus
                                          where s.ParentBotMenuId == null
                                          select  s
                                      ).ToList();
              return getLastSession;
                
            }
        }
        public static List<BotMenu> GetChildrenMenus(int parentMenuId)
        {
            using (AppContext appContext = new AppContext())
            {
                var getLastSession = (
                                      from s in appContext.BotMenus
                                      where s.ParentBotMenuId != null
                                      && s.ParentBotMenuId.ToString() == parentMenuId.ToString()
                                      select s
                                    ).ToList();
                return getLastSession;

            }
        }

        public static BotMenu GetMenu(int botMenuId)
        {
            using (AppContext appContext = new AppContext())
            {
                var getLastSession = (
                                      from s in appContext.BotMenus
                                      where s.BotMenuId == botMenuId
                                      select s
                                    ).FirstOrDefault();
                return getLastSession;

            }
        }
        public static BotMenu GetTreeParentMainRootMenu()
        {
            var menu = GetMenuDefaultHome().FirstOrDefault();
            using (AppContext appContext = new AppContext())
            {
                var getLastSession = (
                                      from s in appContext.BotMenus
                                      where s.BotMenuId == menu.BotMenuId
                                      select s
                                    ).FirstOrDefault();
                return getLastSession;
            }
        }
        public static void DefaultResponseSetMenu(int whatsAppPhoneId)
        {
            using (AppContext appContext = new AppContext())
            {
                var botSession = new BotSession();
                botSession.WhatsAppPhoneId = whatsAppPhoneId;
                botSession.UserScreenText = "0";
                botSession.IsValid = true;
                botSession.ModifiedDate = DateTime.Now;
                botSession.CreatedDate = DateTime.Now;
                botSession.ModifiedBy = "system";
                botSession.CreatedBy = "system";

                appContext.BotSessions.Add(botSession);

                appContext.SaveChanges();
            }
        }
    }
}
