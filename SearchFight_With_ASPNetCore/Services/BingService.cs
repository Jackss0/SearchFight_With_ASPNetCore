using Microsoft.Extensions.Configuration;
using SearchFight_With_ASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight_With_ASPNetCore.Services
{
    class BingService
    {
        private readonly IConfiguration _configuration;

        public BingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void BingWinner(string[] searches)
        {
            var bingResults = BingWebResult(searches).Result;
            var maxResult = bingResults.Count > 0 ? bingResults.Max(t => t.Pages.Results) : 0;
            foreach (var search in bingResults)
            {
                if (search.Pages.Results == maxResult)
                {
                    Printer.Print($"Bing Winner: ");
                    Printer.PrintBlue($"{search.OriginalQuery.Value}");
                    Printer.Print($" With {maxResult}\n");
                }
                break;
            }
        }

        public async Task<List<BingSearch>> BingWebResult(string[] searches)
        {
            HttpClient client = new HttpClient();
            var collection = new List<BingSearch> { };
            try
            {
                foreach (var search in searches)
                {
                    string key = _configuration["bingAPI:key1"];
                    string url = $"https://api.cognitive.microsoft.com/bing/v7.0/search?q= {search}";
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
                    string responseBody = await client.GetStringAsync(url);
                    using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(responseBody)))
                    {
                        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(BingSearch));
                        collection.Add((BingSearch)deserializer.ReadObject(ms));
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return collection;
        }
    }
}
