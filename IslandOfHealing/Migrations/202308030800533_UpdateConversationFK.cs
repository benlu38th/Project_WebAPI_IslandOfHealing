namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConversationFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conversations", "ConcersationsCategoryId", "dbo.Conversations");
            DropForeignKey("dbo.Conversations", "ConversationsCategory_Id", "dbo.ConversationsCategories");
            DropIndex("dbo.Conversations", new[] { "ConcersationsCategoryId" });
            DropIndex("dbo.Conversations", new[] { "ConversationsCategory_Id" });
            RenameColumn(table: "dbo.Conversations", name: "ConversationsCategory_Id", newName: "ConversationsCategoryId");
            AlterColumn("dbo.Conversations", "ConversationsCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Conversations", "ConversationsCategoryId");
            AddForeignKey("dbo.Conversations", "ConversationsCategoryId", "dbo.ConversationsCategories", "Id", cascadeDelete: true);
            DropColumn("dbo.Conversations", "ConcersationsCategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conversations", "ConcersationsCategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Conversations", "ConversationsCategoryId", "dbo.ConversationsCategories");
            DropIndex("dbo.Conversations", new[] { "ConversationsCategoryId" });
            AlterColumn("dbo.Conversations", "ConversationsCategoryId", c => c.Int());
            RenameColumn(table: "dbo.Conversations", name: "ConversationsCategoryId", newName: "ConversationsCategory_Id");
            CreateIndex("dbo.Conversations", "ConversationsCategory_Id");
            CreateIndex("dbo.Conversations", "ConcersationsCategoryId");
            AddForeignKey("dbo.Conversations", "ConversationsCategory_Id", "dbo.ConversationsCategories", "Id");
            AddForeignKey("dbo.Conversations", "ConcersationsCategoryId", "dbo.Conversations", "Id");
        }
    }
}
