namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderStateToOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderStateId", c => c.Int());
            CreateIndex("dbo.Orders", "OrderStateId");
            AddForeignKey("dbo.Orders", "OrderStateId", "dbo.OrderStates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderStateId", "dbo.OrderStates");
            DropIndex("dbo.Orders", new[] { "OrderStateId" });
            DropColumn("dbo.Orders", "OrderStateId");
        }
    }
}
