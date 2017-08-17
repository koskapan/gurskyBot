using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "start";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            using (var db = new ConversationsContext())
            {
                var newConversation = new Conversation()
                {
                    ChatId = message.Chat.Id.ToString(),
                    UserName = $"@{message.From.Username}",
                    State = ConversationState.Common
                };

                db.Conversations.Add(newConversation);

                await db.SaveChangesAsync();

                await client.SendTextMessageAsync(message.Chat.Id, "я запомнил тебя, пёс"); 
            }
        }
    }
}