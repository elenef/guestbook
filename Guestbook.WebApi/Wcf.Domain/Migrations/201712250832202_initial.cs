namespace GuestBook.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisteredUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 255),
                        Login = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false, maxLength: 255),
                        Address = c.String(nullable: false, maxLength: 255),
                        Rating = c.Single(nullable: false),
                        Users_Id = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RegisteredUsers", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Comment = c.String(nullable: false, maxLength: 255),
                        Created = c.DateTime(nullable: false),
                        UserName = c.String(nullable: false),
                        ReviewRating = c.Int(nullable: false),
                        Like = c.Int(nullable: false),
                        Restaurant_Id = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id, cascadeDelete: true)
                .Index(t => t.Restaurant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurants", "Users_Id", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Reviews", "Restaurant_Id", "dbo.Restaurants");
            DropIndex("dbo.Reviews", new[] { "Restaurant_Id" });
            DropIndex("dbo.Restaurants", new[] { "Users_Id" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Restaurants");
            DropTable("dbo.RegisteredUsers");
        }
    }
}
