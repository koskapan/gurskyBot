using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Handlers
{
    public static class CommonTextHandler
    {
        public static async Task Handle(Message message, TelegramBotClient client)
        {
            var messageText = message.Text ?? "";

            if (messageText.ToLower() == "накатим!")
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Нахуй себе накати, опущенец!", replyToMessageId: message.MessageId);
            }
            else if (messageText.ToLower() == "рря!")
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Хуйняяя!", replyToMessageId: message.MessageId);
            }
            else
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Ты чё пиздишь там, сучара?", replyToMessageId: message.MessageId);
            }

        }
    }
}