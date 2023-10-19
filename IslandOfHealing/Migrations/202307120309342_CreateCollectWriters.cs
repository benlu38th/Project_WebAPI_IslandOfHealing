namespace IslandOfHealing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCollectWriters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CollectWriters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FollowerId = c.Int(nullable: false),
                        WriterWhoBeFollowedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.WriterWhoBeFollowedId, cascadeDelete: true)
                .Index(t => t.WriterWhoBeFollowedId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollectWriters", "WriterWhoBeFollowedId", "dbo.Users");
            DropIndex("dbo.CollectWriters", new[] { "WriterWhoBeFollowedId" });
            DropTable("dbo.CollectWriters");
        }
    }
}
