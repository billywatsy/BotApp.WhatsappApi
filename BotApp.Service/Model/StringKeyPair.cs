
using BotApp.Domain.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApp.Service.Model
{
    public class StringKeyPair
    {
        
        public StringKeyPair(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; } 
    }
}
