using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Application.Utils
{
    public class ControllerApiUtil : ControllerBase
    {
        protected JsonStringResult JsonData(Object jsonData, int code)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "dd-MM-yyyy" });
            var json = JsonConvert.SerializeObject(jsonData, serializerSettings);

            return new JsonStringResult(json, code);
        }

        public class JsonStringResult : ContentResult
        {
            public JsonStringResult(string json, int statusCode)
            {
                Content = json;
                ContentType = "application/json";
                StatusCode = statusCode;
            }
        }
    }
}
