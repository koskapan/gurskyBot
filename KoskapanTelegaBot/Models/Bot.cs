
using KoskapanTelegaBot.Models.Commands;
using KoskapanTelegaBot.Models.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;

namespace KoskapanTelegaBot.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;

        private static List<Command> _commands;

        public static HttpClient HttpClient;
        

        public static IReadOnlyList<Command> Commands => _commands.AsReadOnly();
        
        public static async Task<TelegramBotClient> Get()
        {
            try
            {
                if (client != null)
                {
                    return client;
                }
                

                HttpClient = new HttpClient();

                _commands = new List<Command>();
                _commands.Add(new HelloCommand());
                _commands.Add(new BeautyCommand());
                _commands.Add(new TestButtonsCommand());
                _commands.Add(new ReplyButtonsCommand());
                _commands.Add(new StartCommand());
                _commands.Add(new ObidkaCommand());
                _commands.Add(new UsersCommand());

                UpdateHandlerFactory.Register(Telegram.Bot.Types.Enums.UpdateType.MessageUpdate, new MessageHandler());
                UpdateHandlerFactory.Register(Telegram.Bot.Types.Enums.UpdateType.CallbackQueryUpdate, new CallbackQueryHandler());
                UpdateHandlerFactory.Register(Telegram.Bot.Types.Enums.UpdateType.InlineQueryUpdate, new InlineQueryHandler());

                MessageHandlerFactory.Register(Telegram.Bot.Types.Enums.MessageType.TextMessage, new TextMessageHandler());
                MessageHandlerFactory.Register(Telegram.Bot.Types.Enums.MessageType.PhotoMessage, new ImageMessageHandler());

                client = new TelegramBotClient(AppSettings.Key);

                //client.OnCallbackQuery += Client_OnCallbackQuery;
                //client.OnInlineQuery += Client_OnInlineQuery;

                var hook = string.Format(AppSettings.Url, "api/v2/message/update");

                await client.SetWebhookAsync(hook);
                return client;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
    }
}