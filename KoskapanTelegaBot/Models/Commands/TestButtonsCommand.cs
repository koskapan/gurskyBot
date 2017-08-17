using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KoskapanTelegaBot.Models.Commands
{
    public class TestButtonsCommand : Command
    {
        public override string Name => "test";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var keyboard = new InlineKeyboardMarkup()
            {
                InlineKeyboard = new InlineKeyboardButton[][] {
                    new[] {
                        new InlineKeyboardButton("Да", "test-callback-yes"),
                        new InlineKeyboardButton("Нет", "test-callback-no")
                    }
                }
            };

            await client.SendTextMessageAsync(message.Chat.Id, "А не пидор ли ты часом?", replyMarkup: keyboard, parseMode: Telegram.Bot.Types.Enums.ParseMode.Default);
        }
    }
}