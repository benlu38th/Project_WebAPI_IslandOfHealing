namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAIMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AIMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserNickName = c.String(nullable: false, maxLength: 40),
                        UserQuestion = c.String(nullable: false, maxLength: 40),
                        AI = c.Int(nullable: false),
                        AIAnswer = c.String(nullable: false, maxLength: 40),
                        InitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AIMessages", "UserId", "dbo.Users");
            DropIndex("dbo.AIMessages", new[] { "UserId" });
            DropTable("dbo.AIMessages");
        }
    }
}
