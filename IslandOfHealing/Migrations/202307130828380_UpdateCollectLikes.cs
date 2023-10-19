namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollectLikes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Articles", "LikeNum");
            DropColumn("dbo.Articles", "CollectNum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "CollectNum", c => c.Int(nullable: false));
            AddColumn("dbo.Articles", "LikeNum", c => c.Int(nullable: false));
        }
    }
}
