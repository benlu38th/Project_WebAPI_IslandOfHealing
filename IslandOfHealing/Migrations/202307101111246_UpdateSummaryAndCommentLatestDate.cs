namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSummaryAndCommentLatestDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleComments", "LatestDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleComments", "LatestDate");
        }
    }
}
