namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePaidDateAndEndDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PaidDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "EndDate", c => c.DateTime());
            AlterColumn("dbo.Orders", "PaidDate", c => c.DateTime());
        }
    }
}
