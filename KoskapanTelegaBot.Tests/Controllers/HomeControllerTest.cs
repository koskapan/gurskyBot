using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoskapanTelegaBot;
using KoskapanTelegaBot.Controllers;
using KoskapanTelegaBot.Models.Commands;
using Moq;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace KoskapanTelegaBot.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Message()
        {
            var command = new BeautyCommand();
            
            command.Execute(null, null);




        }
    }
}
