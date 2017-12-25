namespace GuestBook.TestCore.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class basetest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExampleModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExampleModels");
        }
    }
}
