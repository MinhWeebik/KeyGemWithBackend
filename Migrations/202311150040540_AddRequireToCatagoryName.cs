namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequireToCatagoryName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Catagories", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Catagories", "Name", c => c.String());
        }
    }
}
