namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idontknow : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Conversations", name: "ConversationsCategoryId", newName: "ConversationCategoryId");
            RenameIndex(table: "dbo.Conversations", name: "IX_ConversationsCategoryId", newName: "IX_ConversationCategoryId");
            AddColumn("dbo.Conversations", "CoverUrl", c => c.String());
            AddColumn("dbo.Conversations", "Summary", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "Summary");
            DropColumn("dbo.Conversations", "CoverUrl");
            RenameIndex(table: "dbo.Conversations", name: "IX_ConversationCategoryId", newName: "IX_ConversationsCategoryId");
            RenameColumn(table: "dbo.Conversations", name: "ConversationCategoryId", newName: "ConversationsCategoryId");
        }
    }
}
