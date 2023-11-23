namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UndoThePreviousChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Variants", "ImageLink", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Variants", "ImageLink", c => c.String(nullable: false));
        }
    }
}
