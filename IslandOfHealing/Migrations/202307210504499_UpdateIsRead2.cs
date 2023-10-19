namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIsRead2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "NotificationContentId", c => c.Int(nullable: false));
            DropColumn("dbo.Notifications", "NotificationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "NotificationId", c => c.Int(nullable: false));
            DropColumn("dbo.Notifications", "NotificationContentId");
        }
    }
}
