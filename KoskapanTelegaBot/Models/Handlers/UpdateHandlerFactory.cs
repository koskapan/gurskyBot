using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot.Types.Enums;

namespace KoskapanTelegaBot.Models.Handlers
{
    public static class UpdateHandlerFactory
    {
        private static Dictionary<UpdateType, IUpdateHandler> _handlers;

        static UpdateHandlerFactory()
        {
            _handlers = new Dictionary<UpdateType, IUpdateHandler>();
        }


        public static void Register(UpdateType type, IUpdateHandler handler, bool force= false)
        {
            if (!_handlers.ContainsKey(type))
            {
                _handlers.Add(type, handler);
            }
            else if (force)
            {
                _handlers[type] = handler;
            }
        }

        public static IUpdateHandler GetHandler(UpdateType type)
        {
            if (_handlers.ContainsKey(type))
            {
                return _handlers[type];
            }
            return null;
        }

    }
}