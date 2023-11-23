namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakePersonIdNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "PersonId", "dbo.People");
            DropIndex("dbo.AspNetUsers", new[] { "PersonId" });
            AlterColumn("dbo.AspNetUsers", "PersonId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "PersonId");
            AddForeignKey("dbo.AspNetUsers", "PersonId", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "PersonId", "dbo.People");
            DropIndex("dbo.AspNetUsers", new[] { "PersonId" });
            AlterColumn("dbo.AspNetUsers", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "PersonId");
            AddForeignKey("dbo.AspNetUsers", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
    }
}
