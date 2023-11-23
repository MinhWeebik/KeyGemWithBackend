namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurnImageListIntoASingleImage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "VariantId", "dbo.Variants");
            DropIndex("dbo.Images", new[] { "VariantId" });
            AddColumn("dbo.Variants", "ImageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Variants", "ImageId");
            AddForeignKey("dbo.Variants", "ImageId", "dbo.Images", "Id", cascadeDelete: true);
            DropColumn("dbo.Images", "VariantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "VariantId", c => c.Int());
            DropForeignKey("dbo.Variants", "ImageId", "dbo.Images");
            DropIndex("dbo.Variants", new[] { "ImageId" });
            DropColumn("dbo.Variants", "ImageId");
            CreateIndex("dbo.Images", "VariantId");
            AddForeignKey("dbo.Images", "VariantId", "dbo.Variants", "Id");
        }
    }
}
