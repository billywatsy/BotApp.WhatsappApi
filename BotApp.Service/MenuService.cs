using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.Service
{
    public class MenuService
    {
        public static JObject Menu()
        {
            JArray menusList = new JArray();

            var menus = BotApp.DataAccess.BotMenuRepository.GetMenuDefaultHome() ?? new List<Domain.Entity.BotMenu>();

            var fieldTypes = BotApp.DataAccess.BotFieldInputTypeRepository.GetList() ?? new List<Domain.Entity.BotFieldInputType>();
             
            JObject menuTree = new JObject();
            menuTree["MainMenu"] = GetSubMenus(menus, fieldTypes);
             string tree = menuTree.ToString();
            return menuTree;
        }
        private static JArray GetSubMenus(List<Domain.Entity.BotMenu> dtMenus , List<Domain.Entity.BotFieldInputType> fieldTypes)
        {
            JArray menus = new JArray();

            foreach (var row in dtMenus)
            {
                JObject menu = JObject.FromObject(row);

                //Fetching sub menus
                menu.Add("SubMenus", GetSubMenus(BotApp.DataAccess.BotMenuRepository.GetChildrenMenus(row.BotMenuId) ?? new List<Domain.Entity.BotMenu>() , fieldTypes));
                //Fetching sub screen
                menu.Add("MenuFields", GetMenuFields(row.BotMenuId, fieldTypes));
                menus.Add(menu);

            }

            return menus;
        }

        public static JArray GetMenuFields(int botMenuId , List<Domain.Entity.BotFieldInputType> fieldTypes)
        {
            JArray fields = new JArray();

            if (botMenuId <= 0)
                return fields;

            var fieldsData = BotApp.DataAccess.BotFieldInputRepository.GetFieldsByMenuId(botMenuId) ?? new List<Domain.Entity.BotFieldInput>();
            
            foreach (var item in fieldsData)
            {
                JObject field = JObject.FromObject(item);
                field["BotFieldInputType"] = JObject.FromObject(GetFieldById(fieldTypes, item.BotFieldInputTypeId));
                fields.Add(field);
            }
            return fields;
        }

        private static Domain.Entity.BotFieldInputType GetFieldById(List<Domain.Entity.BotFieldInputType> fieldTypes , int botFieldInputTypeId)
        {
            if (fieldTypes == null)
                return null;
            return fieldTypes.FirstOrDefault(c => c.BotFieldInputTypeId == botFieldInputTypeId);
        }

    }
}
