namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNotificationSender : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationSenders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ImgUrl = c.String(nullable: false),
                        InitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Notifications", "SenderId", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "NotificationContent", c => c.String(nullable: false));
            CreateIndex("dbo.Notifications", "SenderId");
            AddForeignKey("dbo.Notifications", "SenderId", "dbo.NotificationSenders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "SenderId", "dbo.NotificationSenders");
            DropIndex("dbo.Notifications", new[] { "SenderId" });
            DropColumn("dbo.Notifications", "NotificationContent");
            DropColumn("dbo.Notifications", "SenderId");
            DropTable("dbo.NotificationSenders");
        }
    }
}
