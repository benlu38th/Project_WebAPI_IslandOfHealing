namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateArticleContentImg2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleContentImgs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgUrl = c.String(nullable: false, maxLength: 100),
                        ArticleId = c.Int(),
                        InitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .Index(t => t.ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleContentImgs", "ArticleId", "dbo.Articles");
            DropIndex("dbo.ArticleContentImgs", new[] { "ArticleId" });
            DropTable("dbo.ArticleContentImgs");
        }
    }
}
