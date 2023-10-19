namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExpensePaidField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "Paid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Expenses", "Paid");
        }
    }
}
