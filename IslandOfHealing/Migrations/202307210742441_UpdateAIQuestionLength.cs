namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAIQuestionLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AIMessages", "UserQuestion", c => c.String(nullable: false));
            AlterColumn("dbo.AIMessages", "AIAnswer", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AIMessages", "AIAnswer", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.AIMessages", "UserQuestion", c => c.String(nullable: false, maxLength: 40));
        }
    }
}
