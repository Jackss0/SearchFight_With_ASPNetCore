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

        public GoogleService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<GoogleSearch>> GoogleWebResult(string[] searches)
        {
            HttpClient client = new HttpClient();

            var collection = new List<GoogleSearch>();
            try
            {
                foreach (var search in searches)
                {
                    string key = _configuration["googleAPI:key1"];
                    string url = $"https://app.zenserp.com/api/v2/search?q= {search}&hl=en&gl=US&location=United%20States&search_engine=google.com&apikey={key}";
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
                    string responseBody = await client.GetStringAsync(url);
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
            //var googleResults = GoogleWebResult(searches).Result;

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
