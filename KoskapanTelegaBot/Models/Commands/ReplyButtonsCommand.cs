using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Commands
{
    public class ReplyButtonsCommand : Command
    {
        public override string Name => "rbuttons";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
                var keyboard = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                {
                    Keyboard = new[] {
                                                new[] // row 1
                                                {
                                                    new Telegram.Bot.Types.KeyboardButton("Накатим!"),
                                                    new Telegram.Bot.Types.KeyboardButton("Рря!")
                                                },
                                            },
                    ResizeKeyboard = true
                };

                await client.SendTextMessageAsync(message.Chat.Id, "Давай накатим, товарищ, мой!", false, false, 0, keyboard, Telegram.Bot.Types.Enums.ParseMode.Default);
            
        }
    }
}