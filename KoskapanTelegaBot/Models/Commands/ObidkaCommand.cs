using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Commands
{
    public class ObidkaCommand : Command
    {
        public override string Name => "obidka";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            using (var context = new ConversationsContext())
            {

                var conversation = context.Conversations.FirstOrDefault(c => c.ChatId == message.Chat.Id.ToString());

                if (conversation != null)
                {
                    await client.SendTextMessageAsync(message.Chat.Id, "Пришли мне ссылку на того плебея и сособщение разделённые пробелом");

                    conversation.State = ConversationState.ObidkaWaitName;

                    await context.SaveChangesAsync();
                }
                else
                {

                    await client.SendTextMessageAsync(message.Chat.Id, "Чот я тебя не признал, барсучок. Кинь-ка мне команду /start");
                }


            }
        }
    }
}