namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalStockToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "TotalStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "TotalStock");
        }
    }
}
