namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateOrders2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.String(nullable: false, maxLength: 40),
                        ClientName = c.String(nullable: false, maxLength: 40),
                        ClientEmail = c.String(nullable: false, maxLength: 40),
                        ClientPhone = c.String(nullable: false, maxLength: 15),
                        InitDate = c.DateTime(nullable: false),
                        PaidDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        PlanName = c.String(nullable: false, maxLength: 40),
                        PlanPrice = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PricingPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Features = c.String(maxLength: 150),
                        Price = c.Int(nullable: false),
                        BillingCycle = c.String(nullable: false),
                        InitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropTable("dbo.PricingPlans");
            DropTable("dbo.Orders");
        }
    }
}
