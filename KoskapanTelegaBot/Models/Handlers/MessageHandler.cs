using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Handlers
{
    public class MessageHandler : IUpdateHandler
    {
        public async Task Execute(Update update, TelegramBotClient client)
        {
            var message = update.Message;

            if (message != null)
            {

                var handler = MessageHandlerFactory.GetHandler(message.Type);
                await handler?.Execute(message, client);
            }
        }
    }
}