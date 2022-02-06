using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApp.WhatsappApi.Extensions
{

    public static class NewtownExtension
    {
        private static bool IsKeyEmptyOrNull(this Newtonsoft.Json.Linq.JObject jsonObject, string key)
        {
            if (jsonObject == null || string.IsNullOrEmpty(key))
                return true;
            if (jsonObject[key] == null)
                return true;
            return false;
        }

        /// <summary>
        /// Get Values as string 
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <param name="key">Json propety name </param>
        /// <returns></returns>
        public static string GetJsonObjectStringValue(this Newtonsoft.Json.Linq.JObject jsonObject, string key)
        {
            if (jsonObject.IsKeyEmptyOrNull(key))
                return null;
            return jsonObject[key].ToString();
        }

        public static Newtonsoft.Json.Linq.JObject GetJsonObject(this Newtonsoft.Json.Linq.JObject jsonObject, string key)
        {
            if (jsonObject.IsKeyEmptyOrNull(key))
                return null;
            return jsonObject[key] as Newtonsoft.Json.Linq.JObject;
        }
     
        public static Newtonsoft.Json.Linq.JArray GetJsonArray(this Newtonsoft.Json.Linq.JObject jsonObject, string key)
        {
            if (jsonObject.IsKeyEmptyOrNull(key))
                return null;
            return jsonObject[key] as Newtonsoft.Json.Linq.JArray;
        }
        
        public static Newtonsoft.Json.Linq.JObject GetJsonArrayIndex(this Newtonsoft.Json.Linq.JObject jsonObject, string key , int index)
        {
            if (jsonObject.IsKeyEmptyOrNull(key))
                return null;
            return (jsonObject[key] as Newtonsoft.Json.Linq.JArray)[index] as JObject;
        }

        /// <summary>
        /// 
        //Get Strong type value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObject"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetJsonObject<T>(this Newtonsoft.Json.Linq.JObject jsonObject, string key)
        {
            if (jsonObject.IsKeyEmptyOrNull(key))
                return default(T);

            try
            {
                string value = jsonObject[key].ToString();
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

       
        public static bool IsJsonKeyExist(this Newtonsoft.Json.Linq.JObject jsonObject, string key)
        {
            if (jsonObject.IsKeyEmptyOrNull(key))
                return false;

            try
            {
                if (jsonObject[key] == null)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static JObject GetJobjectFromStringParse (this string data)
        {
            try
            {
                return JObject.Parse(data);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
