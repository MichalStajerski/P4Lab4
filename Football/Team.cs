using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Football
{
    class Team
    {
        [JsonIgnore]
        public int Id { get; set; }
        
        public string School { get; set; }

        public string Abbreviation { get; set; }

        public string Conference { get; set; }

        [JsonIgnore]
        public List<Coach> Coaches { get; set; } = new List<Coach>();



    }
}
