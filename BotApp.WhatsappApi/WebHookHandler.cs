using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotApp.WhatsappApi.Extensions;
using BotApp.DataAccess;

using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;
using BotApp.WhatsappApi.Model;
using BotApp.Domain.Entity;
using BotApp.Domain.Entity.Dto;

namespace BotApp.WhatsappApi
{
    public class WebHookHandler
    {
        static string wa_id = "";
        public static WhatsappResponse Handler(string requestBody )
        {
            ApplicationLogRepository.LogRequest(requestBody);

            var notValidRequest = new BotApp.WhatsappApi.WhatsappResponse("error processing response" , ""); 
            // validte when empty

            if (string.IsNullOrEmpty(requestBody))
                return notValidRequest;

            var requestJSON = requestBody.GetJobjectFromStringParse();

            if (requestJSON == null)
                return notValidRequest;

            var isStatusMessage = requestJSON.IsJsonKeyExist("messages");

            if (!isStatusMessage)
                return notValidRequest;

            wa_id = requestJSON.GetJsonArrayIndex("contacts", 0).GetJsonObjectStringValue("wa_id");
            var bodyTextMessage = requestJSON.GetJsonArrayIndex("messages", 0).GetJsonObject("text").GetJsonObjectStringValue("body");

            var lastMessage = BotSessionRepository.LastActiveMessage(wa_id);

            
            // no message
            if (lastMessage == null)
            {
                var whatsAppPhone =  WhatsAppPhoneRepository.SaveNew(wa_id);
                BotMenuRepository.DefaultResponseSetMenu(whatsAppPhone.WhatsAppPhoneId);
                return new WhatsappResponse(WebHookRender.GetDefaultHomeMenu(), wa_id);
            }
            Newtonsoft.Json.Linq.JObject value = new Newtonsoft.Json.Linq.JObject();
            if (!string.IsNullOrEmpty(lastMessage.DynamicFormFieldJsonObject))
                value = Newtonsoft.Json.Linq.JObject.Parse(lastMessage.DynamicFormFieldJsonObject);
            
            value["wa_id"] = wa_id;

            // check current menuId
            if (lastMessage.BotMenuId >= 1  || (lastMessage.BotFieldInputId == null && lastMessage.BotMenuId == null))
            {
                if (bodyTextMessage == "0")
                {
                    BotMenuRepository.DefaultResponseSetMenu(lastMessage.WhatsAppPhoneId);
                    return new WhatsappResponse(WebHookRender.GetDefaultHomeMenu(), wa_id);
                }

                int.TryParse(bodyTextMessage, out int userInput);

                if (userInput == 0)
                {
                    BotMenuRepository.DefaultResponseSetMenu(lastMessage.WhatsAppPhoneId);
                    return new WhatsappResponse(WebHookRender.GetDefaultHomeMenu(), wa_id);
                }
                var childrenMenusFromPreviousList = new List<BotMenu>();
                if ((lastMessage.BotFieldInputId == null && lastMessage.BotMenuId == null))
                {
                    childrenMenusFromPreviousList = BotMenuRepository.GetMenuDefaultHome();
                }
                else {
                    // loadChildren
                    childrenMenusFromPreviousList = BotMenuRepository.GetChildrenMenus((int)lastMessage.BotMenuId);

                    BotMenu botMenu = BotMenuRepository.GetMenu((int)lastMessage.BotMenuId);
                    if (!string.IsNullOrEmpty(botMenu.PreRenderShowOnlyChildrenHook))
                    {
                        List<string> menusToShow = new List<string>();

                        childrenMenusFromPreviousList = childrenMenusFromPreviousList.Where(c => menusToShow.Contains(c.ThirdPartySystemReference))?.ToList() ?? new List<BotMenu>();
                        if (childrenMenusFromPreviousList == null || childrenMenusFromPreviousList?.Count() <= 0)
                            return new WhatsappResponse("no data found", wa_id);
                    }
                }
                 

                if (childrenMenusFromPreviousList != null && childrenMenusFromPreviousList.Count() >= 1)
                {
                    var nodeValue = childrenMenusFromPreviousList[userInput-1];

                    var childrenMenusListSelected = BotMenuRepository.GetChildrenMenus(nodeValue.BotMenuId);

                    if (childrenMenusListSelected != null && childrenMenusListSelected.Count() >= 1)
                    {
                        
                        BotSessionRepository.SetResponseWhenValidForMenu(nodeValue.BotMenuId, bodyTextMessage, lastMessage.WhatsAppPhoneId);
                        return new WhatsappResponse(WebHookRender.GetMenuRender(childrenMenusListSelected), wa_id);
                    }
                    else
                    {
                        var fields = BotFieldInputRepository.GetFieldsByMenuId(nodeValue.BotMenuId);
                        if (fields == null || fields?.Count() <= 0)
                            return new WhatsappResponse(" no fields set for this menu ", wa_id);
                        return new WhatsappResponse(RenderFieldValue(lastMessage , fields[0].BotFieldInputId, bodyTextMessage , null , lastMessage.WhatsAppPhoneId , ""), wa_id);
                    }
                }
                else
                {
                    return new WhatsappResponse("Main menu does not have children", wa_id);
                }
            }
            else if (lastMessage.BotFieldInputId >= 1)
            {
                var field = BotFieldInputRepository.GetField((int)lastMessage.BotFieldInputId);

                // validate lastMessage.BotFieldInputId
                
                var inputType = BotFieldInputTypeRepository.GetBotFieldInputTypeById(field.BotFieldInputTypeId);
                if (inputType == null)
                    return new WhatsappResponse($"no input type found 01", wa_id);

                if (inputType.Code == "LST")
                {
                    if (!string.IsNullOrEmpty(lastMessage.UserScreenLookUpDataTemplate))
                    {
                        int.TryParse(bodyTextMessage, out int userSelectedOption);
                        var list = Newtonsoft.Json.Linq.JArray.Parse(lastMessage.UserScreenLookUpDataTemplate);

                        if (list != null && list.Count() >= 1 && userSelectedOption >= 1)
                        {
                            Newtonsoft.Json.Linq.JObject indexValue = list[userSelectedOption - 1] as Newtonsoft.Json.Linq.JObject;
                            value = SetFieldCode(value , lastMessage , field , indexValue.GetJsonObjectStringValue("Code"));
                        }
                        else{
                            return new WhatsappResponse($"ERR: GEN293 please select option within range", wa_id);
                        }
                    }
                }
                else
                {
                    // This might be a dynamic menu
                    if (!string.IsNullOrEmpty(lastMessage.UserScreenLookUpDataTemplate))
                    {
                        int.TryParse(bodyTextMessage, out int userSelectedOption);
                        var list = Newtonsoft.Json.Linq.JArray.Parse(lastMessage.UserScreenLookUpDataTemplate);

                        if (list != null && list.Count() >= 1 && userSelectedOption >= 1)
                        {
                            Newtonsoft.Json.Linq.JObject indexValue = list[userSelectedOption - 1] as Newtonsoft.Json.Linq.JObject;
                            value = SetFieldCode(value, lastMessage, field, indexValue.GetJsonObjectStringValue("Code"));
                        }
                        else
                        {
                            return new WhatsappResponse($"ERR: GEN293 please select option within range", wa_id);
                        }
                    }
                    else
                    {
                        value = SetFieldCode(value, lastMessage, field, bodyTextMessage);
                    }
                }
                var fields = BotFieldInputRepository.GetFieldsByMenuId(field.BotMenuId).Select(c => c.BotFieldInputId).ToList();
                var menu = BotMenuRepository.GetMenu(field.BotMenuId);
                int NextValue = 0;
                int PreviousValue = 0;
                int index = fields.FindIndex(nd => nd == field.BotFieldInputId);

                NextValue = fields.ElementAtOrDefault(index + 1);
                PreviousValue = fields.ElementAtOrDefault(index - 1);
                var messageAfterCall = "";
                if (!string.IsNullOrEmpty(field.AfterValidateWebHookUrl))
                {
                   var after  = ExternalWebHook( menu ,field.AfterValidateWebHookUrl, value);

                    messageAfterCall = after?.PreRenderText ?? "";
                }

                if (NextValue > 0)
                {   
                    var responseField = RenderFieldValue(lastMessage , NextValue, bodyTextMessage, value, lastMessage.WhatsAppPhoneId , messageAfterCall );
                    return new WhatsappResponse(responseField, wa_id);
                }
                else
                {
                    BotSessionRepository.SetResponseWhenValidForMenuFinalForms(field.BotMenuId, value , bodyTextMessage , lastMessage.WhatsAppPhoneId );

                    return new WhatsappResponse($" {messageAfterCall  ?? "Form End"} ", wa_id);
                }
            }
            return new WhatsappResponse($"no logic found ", wa_id);
        }

        private static string RenderFieldValue(BotSessionDto botSessionDto , int botFieldInputId, string userScreenText , Newtonsoft.Json.Linq.JObject value , int whatsAppPhoneId , string addFirstText)
        {
            var field = BotFieldInputRepository.GetField(botFieldInputId);

            if (field == null)
                return $" no fields set for this menu ";

            ResponseClientWebHookTemplate preRenderApiCall = null;

            var inputTypeThirdyPartyCode = "";
            var preRenderText = "";
            if (!string.IsNullOrEmpty(field.PreRenderTemplateTextUrl))
            {
                if(value == null) {
                    value = new Newtonsoft.Json.Linq.JObject();
                    value["wa_id"] = wa_id;
                }
                value["__toRenderFieldCode"] = field.FieldCode;
                var menu = BotMenuRepository.GetMenu(field.BotMenuId);
                preRenderApiCall = ExternalWebHook(menu , field.PreRenderTemplateTextUrl, value );

                if (preRenderApiCall.Code != "APP")
                    return (preRenderApiCall.PreRenderText ?? " Error processing request contact ");
                else
                    value = preRenderApiCall.FormValues;
                
                preRenderText = preRenderApiCall.PreRenderText + "\n";
                inputTypeThirdyPartyCode = preRenderApiCall.ThirdyPartFieldMapCode;

                if (!string.IsNullOrEmpty(preRenderText))
                    field.DisplayDescription = preRenderText;

                preRenderApiCall.WaitOnNextFieldCode = botSessionDto.WaitOnNextFieldCode;

            }

            var inputType = BotFieldInputTypeRepository.GetBotFieldInputTypeById(field.BotFieldInputTypeId);
            if (inputType == null)
                return $"EER:GENO1 no field type found internal";

            if (!string.IsNullOrEmpty(inputTypeThirdyPartyCode))
                inputType = BotFieldInputTypeRepository.GetBotFieldInputTypeByCode(inputTypeThirdyPartyCode);

            if (inputType.Code == "TXT")
            {
                BotSessionRepository.SetResponseWhenValidForField(field.BotFieldInputId, userScreenText, value, whatsAppPhoneId , botSessionDto?.ReplaceableFieldCode , botSessionDto.WaitOnNextFieldCode);
                return addFirstText  + (field.DisplayDescription ?? $"Enter  {field.Description} value ");
            } 
            else if (inputType.Code == "DAT")
            {
                BotSessionRepository.SetResponseWhenValidForField( field.BotFieldInputId, userScreenText, value, whatsAppPhoneId ,botSessionDto?.ReplaceableFieldCode , botSessionDto.WaitOnNextFieldCode);
                return addFirstText + ( field.DisplayDescription ?? $"Enter  {field.Description} value \n format below \n yyyy-MMM-dd  ");
            }
            else if (inputType.Code == "LST")
            {
                if (preRenderApiCall == null || preRenderApiCall.DataLookUp == null || preRenderApiCall.DataLookUp.Count() <= 0)
                {
                    return $" no data found for list";
                }

                var responseDataLookUp = WebHookRender.GetUserScreenLookUpDataTemplateRender(preRenderApiCall.DataLookUp , (string.IsNullOrEmpty(field.DisplayDescription) ? "Select "+ field.Description : field.DisplayDescription));

                BotSessionRepository.SetResponseWhenValidForFieldList(field.BotFieldInputId, userScreenText, value, Newtonsoft.Json.Linq.JArray.FromObject(preRenderApiCall.DataLookUp) , whatsAppPhoneId , botSessionDto?.ReplaceableFieldCode , botSessionDto.WaitOnNextFieldCode);
                return  $" \n {responseDataLookUp}";
            } 
            return $" input field not found ";
        }


       
        public static string SendMessage(WhatsappResponse whatsappResponse)
        {
            if (string.IsNullOrEmpty(whatsappResponse.Wa_id))
            {
                return "nothing";
            }

            Newtonsoft.Json.Linq.JObject value = new Newtonsoft.Json.Linq.JObject();
            value["recipient_type"] = "individual";
            value["to"] = whatsappResponse.Wa_id;
            value["type"] = "text";

            Newtonsoft.Json.Linq.JObject valuebody = new Newtonsoft.Json.Linq.JObject();
            valuebody["body"] = whatsappResponse.GeneratedHtml;

            value["text"] = valuebody;


            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0");
            client.DefaultRequestHeaders.Add("D360-API-KEY", "F53S7r_sandbox");

            var uri = new Uri("https://waba-sandbox.360dialog.io/v1/messages");

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var stringContent = new StringContent(value.ToString(), Encoding.UTF8, "application/json");

            var response = client.PostAsync(uri, stringContent).Result;
            var resData = response.Content.ReadAsStringAsync().Result;

            return whatsappResponse.GeneratedHtml;
        }

        public static ResponseClientWebHookTemplate ExternalWebHook(BotMenu menu , string urlCode , Newtonsoft.Json.Linq.JObject formValues )
        {
            string clientWebHookUrl = "";
            if (menu == null)
                return new ResponseClientWebHookTemplate("DEC", "Error processing menu");

            if (string.IsNullOrEmpty(menu.BaseUrl))
                return new ResponseClientWebHookTemplate("DEC", "Error menu does not have base url");

            clientWebHookUrl = menu.BaseUrl + urlCode;

            var menuHeaders = BotMenuFieldInputRequestHeaderRepository.GetListOfHeadersByMenuId(menu.BotMenuId);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var clientRequest = new HttpClient();

            if (menuHeaders != null && menuHeaders?.Count() >= 1)
            {
                foreach(var item in menuHeaders)
                    clientRequest.DefaultRequestHeaders.Add(item.HeaderName, item.HeaderValue);
            }
            else
            {
                clientRequest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientRequest.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0");
            }

            var uri = new Uri(clientWebHookUrl);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var stringContent = new StringContent(formValues.ToString(), Encoding.UTF8, "application/json");

            var response = clientRequest.PostAsync(uri, stringContent).Result;
            var resData = response.Content.ReadAsStringAsync().Result;

            if(response.StatusCode == HttpStatusCode.OK)
            {
                var requestJSON = resData.GetJobjectFromStringParse();
                var replaceableFieldCode = requestJSON.GetJsonObjectStringValue("replaceableFieldCode");
                var thirdyPartFieldMapCode = requestJSON.GetJsonObjectStringValue("thirdyPartFieldMapCode");
                bool.TryParse(requestJSON.GetJsonObjectStringValue("waitOnNextFieldCode") , out bool waitOnNextFieldCode);
                var paramsCode = requestJSON.GetJsonObjectStringValue("code");
                var paramsDescription = requestJSON.GetJsonObjectStringValue("description");
                var paramsPreRenderText = requestJSON.GetJsonObjectStringValue("preRenderText") ?? "";
                formValues["ResultParameters"] = requestJSON.GetJsonArray("resultParameters");
                var paramsDataLookUp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserScreenLookUpDataTemplateDto>>(requestJSON.GetJsonObjectStringValue("dataLookUp"));

                var data = new ResponseClientWebHookTemplate(paramsCode, paramsDescription, paramsPreRenderText, formValues, paramsDataLookUp, thirdyPartFieldMapCode);

                data.WaitOnNextFieldCode = waitOnNextFieldCode;
                data.ReplaceableFieldCode = replaceableFieldCode;
                return data;
            }
            else
            {
                return new ResponseClientWebHookTemplate("DEC", "Error processing form");
            }
        }

        private static Newtonsoft.Json.Linq.JObject SetFieldCode(Newtonsoft.Json.Linq.JObject jObject , BotSessionDto botSessionDto , BotFieldInput botFieldInput , string value)
        {
            if (jObject == null)
                return jObject;
            if(botSessionDto != null && !string.IsNullOrEmpty(botSessionDto?.ReplaceableFieldCode))
                jObject[botSessionDto.ReplaceableFieldCode] = value;
            else
                jObject[botFieldInput.FieldCode] = value;
            return jObject;
        }

         
    }
}


