using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Handlers
{
    public interface IMessageHandler
    {
        Task Execute(Message message, TelegramBotClient client);
    }
}
