namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unknown : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Selected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Selected");
        }
    }
}
