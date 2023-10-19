namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateApplyForWriter2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "WriterProgress", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "ApplyForWriter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ApplyForWriter", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "WriterProgress");
        }
    }
}
