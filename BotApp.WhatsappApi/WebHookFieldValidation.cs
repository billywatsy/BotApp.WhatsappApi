using BotApp.DataAccess;
using BotApp.Domain.Entity;
using BotApp.Domain.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApp.WhatsappApi
{
    public class WebHookFieldValidation
    {
        public static List<string> GetErrors(BotFieldInput botFieldInput , UserScreenLookUpDataTemplateDto selectedFromList, string userInput)
        {
            var listOfValidations = BotFieldInputValidationRepository.GetListByFieldId(botFieldInput.BotFieldInputId);

            if (listOfValidations == null || listOfValidations.Count() <= 0)
                return new List<string>();

            var list = new List<string>(); 
            var inputType = BotFieldInputTypeRepository.GetBotFieldInputTypeById(botFieldInput.BotFieldInputTypeId);

            var isRequired = listOfValidations.FirstOrDefault(c => c.BotFieldValidationTypeCode == "RQD");

            if (isRequired == null && string.IsNullOrEmpty(userInput?.Trim()))
            {
                return new List<string>();
            }
            else
            {

                if (string.IsNullOrEmpty(userInput))
                {
                    if (inputType.Code == "TXT" || inputType.Code == "DAT")
                    {
                        list.Add($"{botFieldInput.Description} is required");
                        return list;
                    }
                }

                if (inputType.Code == "LST")
                {
                    if (selectedFromList == null)
                    {
                        list.Add($"{botFieldInput.Description} is required");
                        return list;
                    }
                }

            }
            foreach (var item in listOfValidations)
            { 
                if (inputType.Code == "TXT")
                {
                   
                        if (item.BotFieldValidationTypeCode == "STRMLN")
                            if (userInput.Length < int.Parse(item.Value))
                                list.Add($"{botFieldInput.Description} should be more than "+ item.Value + " characters");
                        if (item.BotFieldValidationTypeCode == "STRMAX")
                            if (userInput.Length > int.Parse(item.Value))
                                list.Add($"{botFieldInput.Description} should be less than "+ item.Value + " characters");
                        if (item.BotFieldValidationTypeCode == "NUMMLN")
                        {
                            decimal.TryParse(userInput, out decimal minValue);
                            if (minValue < decimal.Parse(item.Value))
                                list.Add($"{botFieldInput.Description} should be greater than " + item.Value + " ");
                        }
                        if (item.BotFieldValidationTypeCode == "NUMMAX")
                        {
                            decimal.TryParse(userInput, out decimal maxValue);
                            if (maxValue > decimal.Parse(item.Value))
                                list.Add($"{botFieldInput.Description} should be less than " + item.Value + " ");
                        }
                    
                }
                else if (inputType.Code == "DAT")
                {
                    if(!DateTime.TryParse(userInput, out DateTime dateValue))
                    {
                        list.Add($"{botFieldInput.Description} date not valid ");
                    }
                }
                else if (inputType.Code == "LST")
                {
                    return new List<string>();
                }
            }
            return list;
        }

    }
}
