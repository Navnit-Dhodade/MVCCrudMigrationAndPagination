namespace NimapTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class task_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryMasters",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.ProductMasters",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.CategoryMasters", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductMasters", "CategoryID", "dbo.CategoryMasters");
            DropIndex("dbo.ProductMasters", new[] { "CategoryID" });
            DropTable("dbo.ProductMasters");
            DropTable("dbo.CategoryMasters");
        }
    }
}
