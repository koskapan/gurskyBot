using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Handlers
{
    public static class ObidkaHandler
    {

        public static async Task Handle(Message message, TelegramBotClient client)
        {
            using (var db = new ConversationsContext())
            {

                var conversation = db.Conversations.FirstOrDefault(c => c.ChatId == message.Chat.Id.ToString());

                if (conversation != null)
                {

                    var messageChunks = message.Text.Split(new[] { ' ' });

                    if (messageChunks.Length > 1)
                    {

                        var userName = messageChunks[0];

                        var messageText = String.Join(" ", messageChunks.Skip(1));

                        var userConversation = db.Conversations.FirstOrDefault(c => c.UserName == userName);

                        if (userConversation == null)
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, "Не знаю этого пидора", replyToMessageId: message.MessageId);

                        }
                        else
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, "Ща всё устроим, барсучок", replyToMessageId: message.MessageId);

                            await client.SendTextMessageAsync(userConversation.ChatId, $"Тут один опущенец (@{message.From.Username}) передал тебе мессагу: {messageText}");
                        }
                    }
                    else
                    {
                        await client.SendTextMessageAsync(message.Chat.Id, "Ты чё, пидр, в шары долбишься?");

                    }

                    conversation.State = ConversationState.Common;

                    await db.SaveChangesAsync();
                }

            }
            
        }
    }
}