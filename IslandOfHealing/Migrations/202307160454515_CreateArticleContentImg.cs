namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateArticleContentImg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractId = c.String(nullable: false, maxLength: 40),
                        UserId = c.Int(nullable: false),
                        PayDate = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        InitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "UserId", "dbo.Users");
            DropIndex("dbo.Expenses", new[] { "UserId" });
            DropTable("dbo.Expenses");
        }
    }
}
