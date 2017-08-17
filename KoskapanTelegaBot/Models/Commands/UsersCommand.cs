using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Commands
{
    public class UsersCommand : Command
    {
        public override string Name => "users";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            using (var db = new ConversationsContext())
            {
                var conversationsStorageCopy = db.Conversations.AsNoTracking().AsEnumerable();

                var usersList = conversationsStorageCopy.Select(c => c.UserName).ToList();

                await client.SendTextMessageAsync(message.Chat.Id, String.Join("\r\n", usersList)); 
            }

        }
    }
}