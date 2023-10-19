namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserUserAI : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UseAI", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UseAI");
        }
    }
}
