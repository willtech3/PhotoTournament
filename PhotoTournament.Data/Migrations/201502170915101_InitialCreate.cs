namespace PhotoTournament.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Winners",
                c => new
                    {
                        WinnerId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        CatPictureUrl = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WinnerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Winners");
        }
    }
}
