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
    public class GoogleService : IGoogleService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        
        public GoogleService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<GoogleSearch>> GoogleWebResult(string[] searches)
        {
            var googleClient = _httpClientFactory.CreateClient();
            
            var collection = new List<GoogleSearch>();
            try
            {
                foreach (var search in searches)
                {
                    var key = _configuration["googleAPI:key1"];
                    var url = _configuration["googleAPI:url"];
                    var commonProps = _configuration["googleAPI:common_props"];
                    var searchEngine = _configuration["googleAPI:search_engine"];

                    var querySearch = $"{url}search?q={search}&search_engine={searchEngine}&apikey={key}";

                    googleClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
                    var responseBody = await googleClient.GetStringAsync(querySearch);

                    using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(responseBody)))
                    {
                        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(GoogleSearch));
                        collection.Add((GoogleSearch)deserializer.ReadObject(ms));
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

        public void GoogleWinner(List<GoogleSearch> googleResults)
        {
            var maxResult = googleResults.Count > 0 ? googleResults.Max(t => t.Results) : 0;
            foreach (var search in googleResults)
            {
                if (search.Results == maxResult)
                {
                    Printer.Print("Google Winner: ");
                    Printer.PrintBlue($"{search.OriginalQuery.Value}");
                    Printer.Print($" With {maxResult}\n");
                }
                break;
            }
        }
    }
}