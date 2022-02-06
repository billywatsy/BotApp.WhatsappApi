using BotApp.DataAccess;
using BotApp.Domain.Entity;
using BotApp.Domain.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApp.Service
{
    public class WebHookRender
    {
        public static string GetMenuRender(List<BotMenu> botMenus, string defaultMainMenu = "Main Menu ")
        {
            if (botMenus == null)
                return "";

            int index = 0;
            List<string> menus = new List<string>();
            menus.Add($"{0} : {defaultMainMenu} \n");
            foreach (var menu in botMenus)
            {
                index++;
                menus.Add($"{index} : {menu.Description} \n");
            }
            return string.Join(" ", menus);
        }

        public static string GetMenuRenderByIndex(List<BotMenu> botMenus , BotSessionDto botSessionDto)
        {
            if (botMenus == null)
                return "";

            int index = 0;
            List<string> menus = new List<string>();
            menus.Add($"{0} : Main Menu \n");
            foreach (var menu in botMenus)
            {
                index++;
                menus.Add($"{index} : {menu.Description} \n");
            }
            return string.Join(" ", menus);
        }


        public static string GetDefaultHomeMenu()
        {
            var botMenus = BotMenuRepository.GetMenuDefaultHome();
            if (botMenus == null)
            {
                return "";
            }

            int index = 0;
            List<string> menus = new List<string>();
            menus.Add($"{0} : Main Menu \n" );
            foreach (var menu in botMenus)
            {
                index++;
                menus.Add($"{index} : {menu.Description} \n");
            }
            return string.Join(" ", menus);
        }
        public static string GetChildrenMenus(int botMenuId)
        {
            var botMenus = BotMenuRepository.GetChildrenMenus(botMenuId);
            if (botMenus == null)
                return "";

            int index = 0;
            List<string> menus = new List<string>();
            menus.Add($"{0} : Main Menu \n");
            foreach (var menu in botMenus)
            {
                index++;
                menus.Add($"{index} : {menu.Description} \n");
            }
            return string.Join(" ", menus);
        }

        public static string GetUserScreenLookUpDataTemplateRender(List<UserScreenLookUpDataTemplateDto> botLookUps , string defaultMainMenu = "Main Menu ")
        {
            if (botLookUps == null)
                return "";

            int index = 0;
            List<string> menus = new List<string>();
            menus.Add($" {defaultMainMenu} \n");
            foreach (var menu in botLookUps)
            {
                index++;
                menus.Add($"{index} : {menu.Description} \n");
            }
            return string.Join(" ", menus);
        }

    }
}
