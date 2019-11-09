using SearchFight_With_ASPNetCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight_With_ASPNetCore.Services
{
    public interface IGoogleService
    {
        Task<List<GoogleSearch>> GoogleWebResult(string[] searches);
        void GoogleWinner(List<GoogleSearch> googleResults);
    }
}
