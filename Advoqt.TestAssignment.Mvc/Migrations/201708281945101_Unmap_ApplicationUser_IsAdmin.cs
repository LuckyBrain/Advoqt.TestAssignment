namespace Advoqt.TestAssignment.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unmap_ApplicationUser_IsAdmin : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "IsAdmin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsAdmin", c => c.Boolean(nullable: false));
        }
    }
}
