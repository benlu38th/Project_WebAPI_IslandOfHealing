namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollectLike : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 200),
                        InitDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 40),
                        Content = c.String(),
                        CoverUrl = c.String(),
                        Pay = c.Boolean(nullable: false),
                        ArticlesCategoryId = c.Int(nullable: false),
                        Summary = c.String(maxLength: 30),
                        Tags = c.String(),
                        Progress = c.Int(nullable: false),
                        InitDate = c.DateTime(nullable: false),
                        LikeNum = c.Int(nullable: false),
                        CollectNum = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticlesCategories", t => t.ArticlesCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ArticlesCategoryId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ArticlesCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                        InitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 40),
                        Password = c.String(nullable: false, maxLength: 40),
                        Salt = c.String(nullable: false),
                        NickName = c.String(maxLength: 40),
                        ImgUrl = c.String(),
                        Role = c.String(nullable: false, maxLength: 15),
                        Birthday = c.DateTime(),
                        MyPlan = c.String(nullable: false),
                        JobTitle = c.String(maxLength: 15),
                        Bio = c.String(),
                        Guid = c.String(),
                        InitDate = c.DateTime(nullable: false),
                        PasswordTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CollectLikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                        Like = c.Boolean(nullable: false),
                        Collect = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollectLikes", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Articles", "ArticlesCategoryId", "dbo.ArticlesCategories");
            DropForeignKey("dbo.ArticleComments", "ArticleId", "dbo.Articles");
            DropIndex("dbo.CollectLikes", new[] { "ArticleId" });
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropIndex("dbo.Articles", new[] { "ArticlesCategoryId" });
            DropIndex("dbo.ArticleComments", new[] { "ArticleId" });
            DropTable("dbo.CollectLikes");
            DropTable("dbo.Users");
            DropTable("dbo.ArticlesCategories");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleComments");
        }
    }
}
