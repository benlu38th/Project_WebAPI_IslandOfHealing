namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateOrders3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PricingPlanId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "PricingPlanId");
            AddForeignKey("dbo.Orders", "PricingPlanId", "dbo.PricingPlans", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "PricingPlanId", "dbo.PricingPlans");
            DropIndex("dbo.Orders", new[] { "PricingPlanId" });
            DropColumn("dbo.Orders", "PricingPlanId");
        }
    }
}
