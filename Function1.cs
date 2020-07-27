using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Dadata;
using Dadata.Model;
using System.Collections.Generic;
using System.Linq;

namespace FunctionApp2
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            var token = "ef2db2da426469acd403d525ff8241bcb5487ef6";
            var api = new GeolocateClient(token);
            int radius = 10;
            List<string> list1 = new List<string>();


            for (int i = 0; i < radius; i++)
            {
                var response = api.Geolocate(lat: (55.7366021), lon: (37.597643));
                var address = response.suggestions[i].data; 
                string full_adress = address.city + " " + address.street + " " + address.house;
                string addr = string.IsNullOrEmpty(address.ToString())
                ? "Address = null"
                : $"Address: {full_adress}";
                list1.Add(addr);
            }


            var bibo = list1.Distinct();
            string list = "_____________________________________________\n";
            foreach(var i in bibo)
            {
                list = list + i + "\n";
            }

           return new OkObjectResult(list);


        }
    }
}
