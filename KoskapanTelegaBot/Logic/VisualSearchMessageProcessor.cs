using KoskapanTelegaBot.Models;
using KoskapanTelegaBot.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KoskapanTelegaBot.Logic
{
    public static class VisualSearchMessageProcessor
    {
        public static Dictionary<long, VisualSearchItem> responses;

        static VisualSearchMessageProcessor()
        {
            responses = new Dictionary<long, VisualSearchItem>();
        }

        public static void RegisterResponse(long chatId, IEnumerable<KuznechVisualSearchResponse> response)
        {
            if (!responses.ContainsKey(chatId))
            {
                responses.Add(chatId, new VisualSearchItem()
                {
                    response= response
                });
            }
            else
            {
                responses[chatId] = new VisualSearchItem()
                {
                    response = response
                }; 
            }
        }

        public static InlineKeyboardMarkup GetUsersCategoryKeyboad(long chatId)
        {
            if (responses.ContainsKey(chatId))
            {
                var userResponse = responses[chatId];

                var inlineKeybordMarkup = new InlineKeyboardMarkup();

                List<InlineKeyboardButton[]> buttonsMatrix = new List<InlineKeyboardButton[]>();

                foreach (var item in userResponse.response)
                {
                    List<InlineKeyboardButton> buttonsRow = new List<InlineKeyboardButton>();

                    var callbackData = $"vscs#{chatId}#{item.Category}";

                    var button = new InlineKeyboardButton(item.Category, callbackData);

                    buttonsRow.Add(button);

                    buttonsMatrix.Add(buttonsRow.ToArray());
                }

                inlineKeybordMarkup.InlineKeyboard = buttonsMatrix.ToArray();

                return inlineKeybordMarkup;
            }
            else
            {
                throw new KeyNotFoundException(chatId.ToString());
            }
        }
        

        public static VisualSearchItemMarkup GetItemKeyboard(long chatId, String category, Int32? num)
        {
            String cloutyProductUrl = "https://www.clouty.ru/{0}/{1}";

            int itemNumber = num ?? 0;

            if (responses.ContainsKey(chatId))
            {
                var categoryItems = responses[chatId].response.FirstOrDefault(c => c.Category.ToLowerInvariant() == category.ToLowerInvariant());

                if (categoryItems != null)
                {
                    var items = categoryItems.Items;

                    if (itemNumber >= 0 && itemNumber < items.Count())
                    {

                        var item = items.ToArray()[itemNumber];

                        var inlineKeyboardMarkup = new InlineKeyboardMarkup()
                        {
                            InlineKeyboard = new InlineKeyboardButton[][]
                            {
                                new[]
                                {
                                    new InlineKeyboardButton(item.Name)
                                    {
                                        Url = $"{String.Format(cloutyProductUrl, item.ID, item.Alias)}"
                                    }
                                },
                                new InlineKeyboardButton[]
                                {
                                }
                            }
                        };

                        var navigationButtons = new List<InlineKeyboardButton>();

                        if (itemNumber > 0)
                        {
                            var neNum = itemNumber-1;
                            var prevButton = new InlineKeyboardButton("<--", $"vsic#{chatId}#{category}#{neNum}");
                            navigationButtons.Add(prevButton);
                        }

                        if (itemNumber < items.Count())
                        {
                            var neNum = itemNumber+1;
                            var nextButton = new InlineKeyboardButton("-->", $"vsic#{chatId}#{category}#{neNum}");
                            navigationButtons.Add(nextButton);
                        }


                        inlineKeyboardMarkup.InlineKeyboard[1] = navigationButtons.ToArray();

                        return new VisualSearchItemMarkup()
                        {
                            Keyboard = inlineKeyboardMarkup,
                            ImageUrl = item.ImageUrl
                        };
                    }
                }
                return null;
            }
            else
            {
                throw new KeyNotFoundException(chatId.ToString());
            }
        }
        

        public static VisualSearchItem GetUsersResponse(long chatId)
        {
            if (responses.ContainsKey(chatId))
            {
                return responses[chatId];
            }
            else
            {
                return null;
            }
        }

        private static String MessageDeleteRequestUrl = "https://api.telegram.org/bot{0}/deleteMessage?chat_id={1}&message_id={2}";

        private static void RemoveMessage(long chatId, String messageId )
        {
            var deleteMessageRequestString = String.Format(MessageDeleteRequestUrl, AppSettings.Key, chatId, messageId);

            var messageDeleteResponse = WebRequest.Create(deleteMessageRequestString);

            using (var deleteResponse = messageDeleteResponse.GetResponse())
            {

            }
        }

        public static async Task ResponseToUser(long chatId, String category, Int32? itemNumber, TelegramBotClient client)
        {
            var userResponse = GetUsersResponse(chatId);

            if (userResponse != null)
            {

                if (String.IsNullOrEmpty(userResponse.CategoryMessageId))
                {
                    var categoryKeyboard = GetUsersCategoryKeyboad(chatId);
                    
                    var categoriesMessage = await client.SendTextMessageAsync(chatId, "Select category:", replyMarkup: categoryKeyboard);

                    userResponse.CategoryMessageId = categoriesMessage.MessageId.ToString();

                    if (!String.IsNullOrEmpty(userResponse.ItemMessageId))
                    {
                        RemoveMessage(chatId, userResponse.ItemMessageId);
                    }

                    userResponse.ItemMessageId = null;
                }
                
                var itemKeyboad = GetItemKeyboard(chatId, category, itemNumber);



                if (itemKeyboad != null)
                {
                    if (String.IsNullOrEmpty(userResponse.ItemMessageId))
                    {

                        var itemMessage = await client.SendTextMessageAsync(chatId, itemKeyboad.ImageUrl, replyMarkup: itemKeyboad.Keyboard);

                        userResponse.ItemMessageId = itemMessage.MessageId.ToString();
                    }
                    else
                    {
                        var itemMessage = await client.EditMessageTextAsync(chatId, Int32.Parse(userResponse.ItemMessageId), itemKeyboad.ImageUrl, replyMarkup: itemKeyboad.Keyboard);

                        userResponse.ItemMessageId = itemMessage.MessageId.ToString();
                    }

                    //var webRequest = WebRequest.Create(itemKeyboad.ImageUrl);

                    //using (var response = webRequest.GetResponse())
                    //using (var content = response.GetResponseStream())
                    //{
                    //    var fileToSend = new FileToSend("picture", content);

                    //    if (!String.IsNullOrEmpty(userResponse.ItemMessageId))
                    //    {
                    //        RemoveMessage(chatId, userResponse.ItemMessageId);
                    //    }

                    //    var itemMessage = await client.SendPhotoAsync(chatId, fileToSend, replyMarkup: itemKeyboad.Keyboard);

                    //    userResponse.ItemMessageId = itemMessage.MessageId.ToString();


                    //}

                }
            }
        }

    }
}