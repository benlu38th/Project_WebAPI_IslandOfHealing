namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderGuidToMerchantOrderNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "MerchantOrderNo", c => c.String(nullable: false, maxLength: 40));
            DropColumn("dbo.Orders", "Guid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Guid", c => c.String(nullable: false, maxLength: 40));
            DropColumn("dbo.Orders", "MerchantOrderNo");
        }
    }
}
