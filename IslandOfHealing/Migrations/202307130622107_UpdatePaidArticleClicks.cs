namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePaidArticleClicks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaidArticleClicks", "InitDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaidArticleClicks", "InitDate");
        }
    }
}
