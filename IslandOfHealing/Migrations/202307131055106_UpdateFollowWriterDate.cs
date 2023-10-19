namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFollowWriterDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FollowWriters", "InitDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FollowWriters", "InitDate");
        }
    }
}
