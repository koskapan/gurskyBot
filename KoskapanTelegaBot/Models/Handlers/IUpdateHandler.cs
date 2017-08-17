using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Handlers
{
    public interface IUpdateHandler
    {
        Task Execute(Update update, TelegramBotClient client);

    }
}