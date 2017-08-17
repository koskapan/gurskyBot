using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KoskapanTelegaBot.Models
{
    public class Conversation
    {
        [Key]
        public String ChatId { get; set; } 

        public String UserName { get; set; }

        public  ConversationState State { get; set; }
    }


    public enum ConversationState
    {
        Common,
        ObidkaWaitName,
    }
}