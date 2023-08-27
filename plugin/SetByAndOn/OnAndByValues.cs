using Newtonsoft.Json;
using System;

namespace ForceOnAndByFields
{
    public class OnAndByValues
    {
        [JsonProperty("By")]
        public string By;

        [JsonProperty("On")]
        public DateTime? On;
        
    }
}
