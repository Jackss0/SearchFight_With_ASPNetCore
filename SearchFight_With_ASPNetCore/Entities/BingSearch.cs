using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SearchFight_With_ASPNetCore.Entities
{

    [DataContract]
    public class BingSearch
    {
        [DataMember(Name = "queryContext")]
        public QueryContext OriginalQuery { get; set; }
        [DataMember(Name = "webPages")]
        public WebPages Pages { get; set; }

        [DataContract]
        public class QueryContext
        {
            [DataMember(Name = "originalQuery")]
            public string Value { get; set; }
        }

        [DataContract]
        public class WebPages
        {
            [DataMember(Name = "totalEstimatedMatches")]
            public long Results { get; set; }
        }


    }

}
