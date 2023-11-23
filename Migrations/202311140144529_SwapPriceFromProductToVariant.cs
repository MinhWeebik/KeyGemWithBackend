namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwapPriceFromProductToVariant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Variants", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.Products", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.Variants", "Price");
        }
    }
}
