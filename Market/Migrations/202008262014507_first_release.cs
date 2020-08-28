namespace Market.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first_release : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        IdCategorie = c.Int(nullable: false, identity: true),
                        NameCategorie = c.String(),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCategorie);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        IdClient = c.Int(nullable: false, identity: true),
                        NameClient = c.String(),
                        Credit = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Observ = c.String(),
                        DateLastCredit = c.DateTime(nullable: false),
                        DateLastRemb = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdClient);
            
            CreateTable(
                "dbo.Emplacers",
                c => new
                    {
                        IdEmplacement = c.Int(nullable: false, identity: true),
                        Emplacement = c.String(),
                    })
                .PrimaryKey(t => t.IdEmplacement);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        IdProduct = c.Int(nullable: false, identity: true),
                        NameProduct = c.String(nullable: false),
                        Img = c.Binary(),
                        Prix = c.Int(nullable: false),
                        PrixSale = c.Int(nullable: false),
                        Qte = c.Int(nullable: false),
                        HasBarCode = c.Boolean(nullable: false),
                        BarCode = c.String(),
                        QteRisque = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        DateSale = c.DateTime(),
                        DatePeremp = c.DateTime(),
                        BuyByPacket = c.Boolean(nullable: false),
                        PrixUnitPacket = c.Int(nullable: false),
                        NumberProductInOnePacket = c.Int(nullable: false),
                        NumberDayAlerte = c.Int(nullable: false),
                        SomeNote = c.String(maxLength: 255),
                        IdCategorie = c.Int(),
                        Emplacer_IdEmplacement = c.Int(),
                    })
                .PrimaryKey(t => t.IdProduct)
                .ForeignKey("dbo.Categories", t => t.IdCategorie)
                .ForeignKey("dbo.Emplacers", t => t.Emplacer_IdEmplacement)
                .Index(t => t.IdCategorie)
                .Index(t => t.Emplacer_IdEmplacement);
            
            CreateTable(
                "dbo.Remboursements",
                c => new
                    {
                        DateRemb = c.DateTime(nullable: false),
                        Value = c.Int(nullable: false),
                        Observ = c.String(),
                        Client_IdClient = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DateRemb)
                .ForeignKey("dbo.Clients", t => t.Client_IdClient, cascadeDelete: true)
                .Index(t => t.Client_IdClient);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        IdSetting = c.Int(nullable: false, identity: true),
                        Version = c.String(),
                        IdLastBuy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSetting);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        NameUser = c.String(nullable: false, maxLength: 50),
                        Password = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdUser);
            
            CreateTable(
                "dbo.BuyDetail",
                c => new
                    {
                        BuyDetailID = c.Int(nullable: false),
                        IdBuyForeignKey = c.Int(nullable: false),
                        IdProductForeignKey = c.Int(),
                        Qte = c.Int(),
                        PrixUnit = c.Int(),
                        PrixTotal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BuyDetailID, t.IdBuyForeignKey });
            
            CreateTable(
                "dbo.Buy",
                c => new
                    {
                        BuyID = c.Int(nullable: false, identity: true),
                        Montant = c.Int(nullable: false),
                        DateBuy = c.DateTime(nullable: false),
                        Client_IdClient = c.Int(),
                        User_IdUser = c.Int(),
                    })
                .PrimaryKey(t => t.BuyID)
                .ForeignKey("dbo.Clients", t => t.Client_IdClient)
                .ForeignKey("dbo.Users", t => t.User_IdUser)
                .Index(t => t.Client_IdClient)
                .Index(t => t.User_IdUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Buy", "User_IdUser", "dbo.Users");
            DropForeignKey("dbo.Buy", "Client_IdClient", "dbo.Clients");
            DropForeignKey("dbo.Remboursements", "Client_IdClient", "dbo.Clients");
            DropForeignKey("dbo.Products", "Emplacer_IdEmplacement", "dbo.Emplacers");
            DropForeignKey("dbo.Products", "IdCategorie", "dbo.Categories");
            DropIndex("dbo.Buy", new[] { "User_IdUser" });
            DropIndex("dbo.Buy", new[] { "Client_IdClient" });
            DropIndex("dbo.Remboursements", new[] { "Client_IdClient" });
            DropIndex("dbo.Products", new[] { "Emplacer_IdEmplacement" });
            DropIndex("dbo.Products", new[] { "IdCategorie" });
            DropTable("dbo.Buy");
            DropTable("dbo.BuyDetail");
            DropTable("dbo.Users");
            DropTable("dbo.Settings");
            DropTable("dbo.Remboursements");
            DropTable("dbo.Products");
            DropTable("dbo.Emplacers");
            DropTable("dbo.Clients");
            DropTable("dbo.Categories");
        }
    }
}
