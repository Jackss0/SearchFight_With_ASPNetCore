using System.Runtime.Serialization;

namespace SearchFight_With_ASPNetCore.Entities
{
    [DataContract]
    public class GoogleSearch
    {
        [DataMember(Name = "query")]
        public Query OriginalQuery { get; set; }
        [DataMember(Name = "number_of_results")]
        public long Results { get; set; }

        [DataContract]
        public class Query
        {
            [DataMember(Name = "q")]
            public string Value { get; set; }
        }
    }
}
