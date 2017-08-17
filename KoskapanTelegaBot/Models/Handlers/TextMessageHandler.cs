using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Handlers
{
    public class TextMessageHandler : IMessageHandler
    {
        public async Task Execute(Message message, TelegramBotClient client)
        {
            var commands = Bot.Commands;

            var handled = false;

            if (message != null && !String.IsNullOrEmpty(message.Text))
            {
                foreach (var command in commands)
                {
                    if (command.Contains(message.Text))
                    {
                        await command.Execute(message, client);
                        handled = true;
                        break;
                    }
                }
            }
            if (!handled)
            {
                await Handle(message, client);
            }
        }

        public async Task Handle(Message message, TelegramBotClient client)
        {
            using (var db = new ConversationsContext())
            {

                var conversation = db.Conversations.FirstOrDefault(c => c.ChatId == message.Chat.Id.ToString());

                if (conversation != null && conversation.State == ConversationState.ObidkaWaitName)
                {
                    await ObidkaHandler.Handle(message, client);
                }
                else
                {

                    await CommonTextHandler.Handle(message, client);
                }

            }

        }
    }
}