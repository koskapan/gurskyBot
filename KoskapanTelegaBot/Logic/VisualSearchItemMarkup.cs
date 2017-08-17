using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot.Types.ReplyMarkups;

namespace KoskapanTelegaBot.Logic
{
    public class VisualSearchItemMarkup
    {
        public String ImageUrl { get; set; }
        public InlineKeyboardMarkup Keyboard { get; set; }
    }
}