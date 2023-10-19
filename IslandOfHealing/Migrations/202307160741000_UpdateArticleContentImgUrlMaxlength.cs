namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateArticleContentImgUrlMaxlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArticleContentImgs", "ImgUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ArticleContentImgs", "ImgUrl", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
