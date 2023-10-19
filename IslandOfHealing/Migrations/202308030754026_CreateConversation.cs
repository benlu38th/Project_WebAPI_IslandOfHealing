namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateConversation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConversationComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 200),
                        InitDate = c.DateTime(nullable: false),
                        LatestDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ConversationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .Index(t => t.ConversationId);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 40),
                        Content = c.String(),
                        Tags = c.String(),
                        Anonymous = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        ConcersationsCategoryId = c.Int(nullable: false),
                        InitDate = c.DateTime(nullable: false),
                        ConversationsCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversations", t => t.ConcersationsCategoryId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.ConversationsCategories", t => t.ConversationsCategory_Id)
                .Index(t => t.UserId)
                .Index(t => t.ConcersationsCategoryId)
                .Index(t => t.ConversationsCategory_Id);
            
            CreateTable(
                "dbo.ConversationsCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                        Description = c.String(nullable: false, maxLength: 200),
                        InitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversations", "ConversationsCategory_Id", "dbo.ConversationsCategories");
            DropForeignKey("dbo.ConversationComments", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Conversations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Conversations", "ConcersationsCategoryId", "dbo.Conversations");
            DropIndex("dbo.Conversations", new[] { "ConversationsCategory_Id" });
            DropIndex("dbo.Conversations", new[] { "ConcersationsCategoryId" });
            DropIndex("dbo.Conversations", new[] { "UserId" });
            DropIndex("dbo.ConversationComments", new[] { "ConversationId" });
            DropTable("dbo.ConversationsCategories");
            DropTable("dbo.Conversations");
            DropTable("dbo.ConversationComments");
        }
    }
}
