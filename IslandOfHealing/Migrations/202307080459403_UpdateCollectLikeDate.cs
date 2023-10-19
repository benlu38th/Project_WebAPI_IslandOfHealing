namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCollectLikeDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CollectLikes", "LikeDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CollectLikes", "CollectDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CollectLikes", "CollectDate");
            DropColumn("dbo.CollectLikes", "LikeDate");
        }
    }
}
