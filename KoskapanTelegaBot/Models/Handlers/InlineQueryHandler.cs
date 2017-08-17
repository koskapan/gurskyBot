using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;

namespace KoskapanTelegaBot.Models.Handlers
{
    public class InlineQueryHandler : IUpdateHandler
    {
        public async Task Execute(Update update, TelegramBotClient client)
        {
            var query = update.InlineQuery.Query;

            var msg = new InputTextMessageContent()
            {
                MessageText = @"Пидр",
                ParseMode = Telegram.Bot.Types.Enums.ParseMode.Html
            };

            Telegram.Bot.Types.InlineQueryResults.InlineQueryResult[] results = {
                        new Telegram.Bot.Types.InlineQueryResults.InlineQueryResultArticle
                        {
                            Id = "1",
                            Title = "Тестовый тайтл",
                            Description = "Описание статьи тут",
                            InputMessageContent = msg,
                        },
                        new Telegram.Bot.Types.InlineQueryResults.InlineQueryResultPhoto
                        {
                            Id = "2",
                            Url = "http://aftamat4ik.ru/wp-content/uploads/2017/05/14277366494961.jpg",
                            ThumbUrl = "http://aftamat4ik.ru/wp-content/uploads/2017/05/14277366494961-150x150.jpg",
                            Caption = "Текст под фоткой",
                            Description = "Описание"
                        },
                        new Telegram.Bot.Types.InlineQueryResults.InlineQueryResultAudio
                        {
                            Url = "http://aftamat4ik.ru/wp-content/uploads/2017/05/mongol-shuudan_-_kozyr-nash-mandat.mp3",
                            Id = "3",
                            FileId = "123423433",
                            Title = "Монгол Шуудан - Козырь наш Мандат!"
                        },
                        new Telegram.Bot.Types.InlineQueryResults.InlineQueryResultVideo
                        {
                            Id = "4",
                            Url = "http://aftamat4ik.ru/wp-content/uploads/2017/05/bb.mp4",
                            ThumbUrl = "http://aftamat4ik.ru/wp-content/uploads/2017/05/joker_5-150x150.jpg",
                            Title = "демо видеоролика",
                            MimeType = "video/mp4",
                        }
                    };

            await client.AnswerInlineQueryAsync(update.InlineQuery.Id, results);
        }
    }
}