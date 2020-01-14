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
        private readonly IHttpClientFactory _httpClientFactory;

        public BingService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
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
            var bingClient = _httpClientFactory.CreateClient();
            var collection = new List<BingSearch>();
            try
            {
                foreach (var search in searches)
                {
                    var key = _configuration["bingAPI:key1"];
                    var url = _configuration["bingAPI:url"];

                    var querySearch = $"{url}search?q={search}";
                    bingClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
                    var responseBody = await bingClient.GetStringAsync(querySearch);

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
