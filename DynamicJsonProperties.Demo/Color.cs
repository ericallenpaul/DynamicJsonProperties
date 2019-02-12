using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicJsonProperties.Demo
{
    public partial class Color
    {
        [JsonProperty("color")]
        public string ColorColor { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; } = "N/A";

        [JsonProperty("code")]
        public Code Code { get; set; }
    }
}
