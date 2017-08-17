using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoskapanTelegaBot.Models.Types
{
    public class KuznechVisualSearchResponse
    {
        [JsonProperty("kuznechCategory")]
        public String Category { get; set; }

        //[JsonProperty("rectangle")]
        //public KuznechVisualSearchRectangle Rectangle {get;set;}

        [JsonProperty("items")]
        public IEnumerable<KuznechVisualSearchItem> Items { get; set; }
    }


    public class KuznechVisualSearchItem
    {
        [JsonProperty("id")]
        public Guid ID { get; set; }

        [JsonProperty("alias")]
        public String Alias { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("imageUrl")]
        public String ImageUrl { get; set; }
    }

    public class KuznechVisualSearchRectangle
    {
        [JsonProperty("height")]
        public Int32 Height { get; set; }

        [JsonProperty("width")]
        public Int32 Width { get; set; }

        [JsonProperty("x")]
        public Int32 XAxis { get; set; }

        [JsonProperty("y")]
        public Int32 YAxis { get; set; }
    }
}