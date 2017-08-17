using KoskapanTelegaBot.Logic;
using KoskapanTelegaBot.Models.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KoskapanTelegaBot.Models.Handlers
{
    public class ImageMessageHandler : IMessageHandler
    {
        private String imagePath = "https://api.telegram.org/file/bot{0}/{1}";
        private String productVisualSearchPath = "https://api.clouty.ru/v1/search/detect/url?url={0}";

        public async Task Execute(Message message, TelegramBotClient client)
        {
            var photo = message.Photo;

            var biggestFile = photo.Max(p => p.FileSize);

            var biggestFileId = photo.FirstOrDefault(p => p.FileSize == biggestFile).FileId;

            var file = await client.GetFileAsync(biggestFileId);

            var path = file.FilePath;

            var formattedFilePath = String.Format(imagePath, AppSettings.Key, path);

            var formattedVisualSearchRequest = String.Format(productVisualSearchPath, formattedFilePath);



            await client.SendTextMessageAsync(message.Chat.Id, $"Processing visual search request");
            try
            {

                var vsWebRequest = WebRequest.Create(formattedVisualSearchRequest);

                var resObj = new List<KuznechVisualSearchResponse>()
                {
                    new KuznechVisualSearchResponse() {
                        Items = new List<KuznechVisualSearchItem>()
                    }
                };

                using (var response = vsWebRequest.GetResponse())
                using (var content = response.GetResponseStream())
                using (var reader = new StreamReader(content))
                {
                    var resStr = await reader.ReadToEndAsync();

                    //await client.SendTextMessageAsync(message.Chat.Id, $"Got visual response string");

                    resObj = JsonConvert.DeserializeObject<List<KuznechVisualSearchResponse>>(resStr);

                    //await client.SendTextMessageAsync(message.Chat.Id, $"Converted response string to object");
                }

                VisualSearchMessageProcessor.RegisterResponse(message.Chat.Id, resObj);

                await VisualSearchMessageProcessor.ResponseToUser(message.Chat.Id, "", 1, client);


            }
            catch (Exception ex)
            {

                await client.SendTextMessageAsync(message.Chat.Id, $"Got exception: {ex.ToString()}");
            }
            //await client.SendTextMessageAsync(message.Chat.Id, $"Got visual search response {resStr}");
        }
    }
}