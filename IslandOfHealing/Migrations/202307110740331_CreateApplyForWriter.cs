namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateApplyForWriter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ApplyForWriter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ApplyForWriter");
        }
    }
}
