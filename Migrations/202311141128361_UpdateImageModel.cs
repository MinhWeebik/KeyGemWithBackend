namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateImageModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Images", "ImageLink", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Images", "ImageLink", c => c.String());
        }
    }
}
