namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNotificationNewArticleInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notifications", "FollowedWriterNewArticleId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notifications", "FollowedWriterNewArticleId", c => c.Int(nullable: false));
        }
    }
}
