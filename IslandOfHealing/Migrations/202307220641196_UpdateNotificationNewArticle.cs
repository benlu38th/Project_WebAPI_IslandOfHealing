namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNotificationNewArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "FollowedWriterNewArticleId", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "FollowedWriterNewArticleTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "FollowedWriterNewArticleTitle");
            DropColumn("dbo.Notifications", "FollowedWriterNewArticleId");
        }
    }
}
