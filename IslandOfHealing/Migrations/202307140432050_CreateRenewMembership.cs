namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRenewMembership : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RenewMembership", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "RenewMembership");
        }
    }
}
