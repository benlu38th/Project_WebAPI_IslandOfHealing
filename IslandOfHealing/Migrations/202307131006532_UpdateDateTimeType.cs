namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDateTimeType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CollectLikes", "LikeDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CollectLikes", "CollectDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CollectLikes", "CollectDate", c => c.DateTime());
            AlterColumn("dbo.CollectLikes", "LikeDate", c => c.DateTime());
        }
    }
}
