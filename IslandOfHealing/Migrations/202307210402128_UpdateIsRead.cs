namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIsRead : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "IsRead", c => c.Boolean(nullable: false));
            DropColumn("dbo.Notifications", "Read");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Read", c => c.Boolean(nullable: false));
            DropColumn("dbo.Notifications", "IsRead");
        }
    }
}
