namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePaidArticleClicks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaidArticleClicks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClickerId = c.Int(nullable: false),
                        PaidArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.PaidArticleId, cascadeDelete: true)
                .Index(t => t.PaidArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaidArticleClicks", "PaidArticleId", "dbo.Articles");
            DropIndex("dbo.PaidArticleClicks", new[] { "PaidArticleId" });
            DropTable("dbo.PaidArticleClicks");
        }
    }
}
