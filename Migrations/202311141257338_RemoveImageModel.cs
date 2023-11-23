namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveImageModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Variants", "ImageId", "dbo.Images");
            DropIndex("dbo.Variants", new[] { "ImageId" });
            AddColumn("dbo.Variants", "ImageLink", c => c.String());
            DropColumn("dbo.Variants", "ImageId");
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageLink = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Variants", "ImageId", c => c.Int(nullable: false));
            DropColumn("dbo.Variants", "ImageLink");
            CreateIndex("dbo.Variants", "ImageId");
            AddForeignKey("dbo.Variants", "ImageId", "dbo.Images", "Id", cascadeDelete: true);
        }
    }
}
