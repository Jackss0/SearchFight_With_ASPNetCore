using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight_With_ASPNetCore.Entities
{
    public class Winner
    {
        public string Query { get; set; }
        public long Result { get; set; }

        public Winner(string query, long result)
        {
            Query = query;
            Result = result;
        }
    }
}
