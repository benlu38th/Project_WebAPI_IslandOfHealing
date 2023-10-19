namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConversationContentImg2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConversationContentImgs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgUrl = c.String(nullable: false),
                        ConversationId = c.Int(),
                        InitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversations", t => t.ConversationId)
                .Index(t => t.ConversationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConversationContentImgs", "ConversationId", "dbo.Conversations");
            DropIndex("dbo.ConversationContentImgs", new[] { "ConversationId" });
            DropTable("dbo.ConversationContentImgs");
        }
    }
}
