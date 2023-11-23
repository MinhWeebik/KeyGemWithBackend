namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TurnOrderStateToNonNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderStateId", "dbo.OrderStates");
            DropIndex("dbo.Orders", new[] { "OrderStateId" });
            AlterColumn("dbo.Orders", "OrderStateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "OrderStateId");
            AddForeignKey("dbo.Orders", "OrderStateId", "dbo.OrderStates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderStateId", "dbo.OrderStates");
            DropIndex("dbo.Orders", new[] { "OrderStateId" });
            AlterColumn("dbo.Orders", "OrderStateId", c => c.Int());
            CreateIndex("dbo.Orders", "OrderStateId");
            AddForeignKey("dbo.Orders", "OrderStateId", "dbo.OrderStates", "Id");
        }
    }
}
