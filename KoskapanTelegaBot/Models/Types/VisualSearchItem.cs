using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoskapanTelegaBot.Models.Types
{
    public class VisualSearchItem
    {
        public IEnumerable<KuznechVisualSearchResponse> response { get; set; }
        public String CategoryMessageId { get; set; }
        public String ItemMessageId { get; set; }
    }
}