using KoskapanTelegaBot.Models;
using KoskapanTelegaBot.Models.Handlers;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;

namespace KoskapanTelegaBot.Controllers
{
    [Route(@"api/v2/message/update")]
    public class MessageController : ApiController
    {
        static ILogger _logger = LogManager.GetCurrentClassLogger();

        public async Task<OkResult> Update([FromBody]Update update )
        {
            var client = await Bot.Get();

            try
            {
                var handler = UpdateHandlerFactory.GetHandler(update.Type);
                await handler?.Execute(update, client);
            }
            catch (Exception ex)
            {
                _logger.Trace(ex, "error was handled");
            }

            return Ok();
        }

    }
}
