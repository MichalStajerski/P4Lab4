using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Football
{
    class Coach
    {
        [JsonIgnore]
        public int Id { get; set; }
        
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        public List<Season> Seasons { get; set; }

        public class Season
        {
            [JsonIgnore]
            public int Id { get; set; }
            public string School { get; set; }
        }

    }
}
