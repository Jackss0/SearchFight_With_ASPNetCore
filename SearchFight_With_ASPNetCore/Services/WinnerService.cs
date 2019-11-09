using SearchFight_With_ASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchFight_With_ASPNetCore.Services
{
    public class WinnerService
    {
        private readonly GoogleService _googleService;

        public WinnerService(GoogleService googleService)
        {
            _googleService = googleService;
        }

        public void TotalWinner(string[] searches)
        {
            var googleSearches = _googleService.GoogleWebResult(searches).Result;
            _googleService.GoogleWinner(googleSearches);
            //var engineAndResults = bingSearches.Zip(googleSearches, (b, g) => new { BingSearch = b, GoogleSearch = g }).ToList();
            var maxSumResults = googleSearches.GroupBy(
                                                         e => e.OriginalQuery.Value)
                                                         .Select(s => new Winner(
                                                             s.First().OriginalQuery.Value,
                                                             s.Max(c => c.Results)
                                                             ));
            var totalMax = maxSumResults.Count() > 0 ? maxSumResults.Max(t => t.Result) : 0;
            foreach (var winner in maxSumResults)
            {
                if (winner.Result == totalMax)
                {
                    Printer.Print($"Total Winner: ");
                    Printer.PrintYellow($"\"{winner.Query}\"");
                    Printer.Print(" with ");
                    Printer.PrintGray($"{(winner.Result)}");
                    Printer.Print(" results in total.\n");
                }

            }
        }
    }
}
