using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot.Types.Enums;

namespace KoskapanTelegaBot.Models.Handlers
{
    public static class MessageHandlerFactory
    {
        private static Dictionary<MessageType, IMessageHandler> _handlers = new Dictionary<MessageType, IMessageHandler>();

        public static void Register(MessageType type, IMessageHandler handler, bool force = false)
        {
            if (!_handlers.ContainsKey(type))
            {
                _handlers.Add(type, handler);
            }
            else
            {
                if (force)
                {
                    _handlers[type] = handler;
                }
            }
        }

        public static IMessageHandler GetHandler(MessageType type)
        {
            if (_handlers.ContainsKey(type))
            {
                return _handlers[type];
            }
            return null;
        }

    }
}