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

        public static void SearchesPerEngine(List<GoogleSearch> googleSearches)
        {
            foreach (var engines in googleSearches)
            {
                Printer.PrintYellow($"\"{engines.OriginalQuery.Value}\"");
                Printer.Print(" searchs on ");
                Printer.PrintBlue($"Google: ");
                Printer.PrintGray($"{engines.Results}");
                Printer.Print(" results");
                Console.WriteLine();
            }
        }

        public void TotalWinner(string[] searches)
        {
            var googleSearches = _googleService.GoogleWebResult(searches).Result;
            SearchesPerEngine(googleSearches);
            _googleService.GoogleWinner(googleSearches);

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
