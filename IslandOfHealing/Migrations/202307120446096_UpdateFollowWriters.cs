namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFollowWriters : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CollectWriters", newName: "FollowWriters");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.FollowWriters", newName: "CollectWriters");
        }
    }
}
