using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace KoskapanTelegaBot.Models
{
    public class ConversationsContext : DbContext
    {
        public ConversationsContext() : base("DefaultConnection") { }

        public DbSet<Conversation> Conversations { get; set; }

    }
}