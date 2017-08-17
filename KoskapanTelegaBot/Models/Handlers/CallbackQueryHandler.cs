using KoskapanTelegaBot.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Handlers
{
    public class CallbackQueryHandler: IUpdateHandler
    {
        public async Task Execute(Update update, TelegramBotClient client)
        {
            var e = update.CallbackQuery;

            try
            {
                var message = e.Message;
                var queryAnswered = false;

                if (e.Data == "test-callback-no")
                {
                    queryAnswered = await client.AnswerCallbackQueryAsync(e.Id, "Пидора ответ", true);
                }
                else if (e.Data == "test-callback-yes")
                {
                    queryAnswered = await client.AnswerCallbackQueryAsync(e.Id, "Так то лучше, сучара", true);
                }
                else if (e.Data.StartsWith("vscs"))
                {
                    var chunks = e.Data.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);


                    await VisualSearchMessageProcessor.ResponseToUser(Int64.Parse(chunks[1]), chunks[2], 0, client);

                }
                else if (e.Data.StartsWith("vsic"))
                {
                    var chunks = e.Data.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

                    var itemNumber = Int32.Parse(chunks[3]);

                    await VisualSearchMessageProcessor.ResponseToUser(Int64.Parse(chunks[1]), chunks[2], itemNumber, client);
                }
                else
                {
                    queryAnswered = await client.AnswerCallbackQueryAsync(e.Id, "Нихуя не понел, пёс", true);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}