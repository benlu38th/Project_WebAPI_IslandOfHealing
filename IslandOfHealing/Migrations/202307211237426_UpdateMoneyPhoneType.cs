namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMoneyPhoneType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ClientPhone", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ClientPhone", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
