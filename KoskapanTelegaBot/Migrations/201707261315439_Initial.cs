namespace KoskapanTelegaBot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        ChatId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChatId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Conversations");
        }
    }
}
