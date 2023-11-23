namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredToImageLink : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Variants", "ImageLink", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Variants", "ImageLink", c => c.String());
        }
    }
}
