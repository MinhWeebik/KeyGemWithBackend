﻿namespace KeyGem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderStateModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderStates");
        }
    }
}
