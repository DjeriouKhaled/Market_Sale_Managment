namespace Market.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table_setting_edit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Settings", "IdLastBuy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "IdLastBuy", c => c.Int(nullable: false));
        }
    }
}
