using System;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Models.Commands
{
    public class BeautyCommand : Command
    {
        public override string Name => "beauty";

        private static String[] Links;
        private static String[] Descriptions;

        static Random rnd = new Random(Environment.TickCount);
        
        public override async Task Execute(Message message, TelegramBotClient client)
        {

            var chatId = message.Chat.Id;

            using (var webClient = new WebClient())
            {
                var uri = new Uri("https://raw.githubusercontent.com/koskapan/gurskyBotPictureLinks/master/links.txt");
                string s = webClient.DownloadString(uri);
                Links = s.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }

            using (var webClient = new WebClient())
            {
                var uri = new Uri("https://raw.githubusercontent.com/koskapan/gurskyBotPictureLinks/master/descriptions.txt");
                string s = webClient.DownloadString(uri);
                Descriptions = s.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }



            var linkId = rnd.Next(Links.Length);
            var descId = rnd.Next(Descriptions.Length);
            var picLink = Links[linkId];
            var desc = Descriptions[descId];
            var webRequest = WebRequest.Create(picLink);

            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            {
                var fileToSend = new FileToSend("GurskyOneLove", content);
                
                await client.SendPhotoAsync(chatId, fileToSend, desc);
            }
        }
    }
}