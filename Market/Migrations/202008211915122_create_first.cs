namespace Market.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_first : DbMigration
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
                "dbo.BuyDetails",
                c => new
                    {
                        IdVentDetail = c.Int(nullable: false),
                        IdVent_ForeignKey = c.Int(nullable: false),
                        IdProduct_ForeignKey = c.Int(nullable: false),
                        Qte = c.Int(nullable: false),
                        PrixUnit = c.Int(nullable: false),
                        PrixTotal = c.Int(nullable: false),
                        Product_IdProduct = c.Int(),
                        Vent_IdVent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdVentDetail, t.IdVent_ForeignKey })
                .ForeignKey("dbo.Products", t => t.Product_IdProduct)
                .ForeignKey("dbo.Buys", t => t.Vent_IdVent, cascadeDelete: true)
                .Index(t => t.Product_IdProduct)
                .Index(t => t.Vent_IdVent);
            
            CreateTable(
                "dbo.Buys",
                c => new
                    {
                        IdVent = c.Int(nullable: false, identity: true),
                        Montant = c.Int(nullable: false),
                        DateVent = c.DateTime(nullable: false),
                        Client_IdClient = c.Int(),
                        User_IdUser = c.Int(),
                    })
                .PrimaryKey(t => t.IdVent)
                .ForeignKey("dbo.Clients", t => t.Client_IdClient)
                .ForeignKey("dbo.Users", t => t.User_IdUser)
                .Index(t => t.Client_IdClient)
                .Index(t => t.User_IdUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuyDetails", "Vent_IdVent", "dbo.Buys");
            DropForeignKey("dbo.Buys", "User_IdUser", "dbo.Users");
            DropForeignKey("dbo.Buys", "Client_IdClient", "dbo.Clients");
            DropForeignKey("dbo.BuyDetails", "Product_IdProduct", "dbo.Products");
            DropForeignKey("dbo.Remboursements", "Client_IdClient", "dbo.Clients");
            DropForeignKey("dbo.Products", "Emplacer_IdEmplacement", "dbo.Emplacers");
            DropForeignKey("dbo.Products", "IdCategorie", "dbo.Categories");
            DropIndex("dbo.Buys", new[] { "User_IdUser" });
            DropIndex("dbo.Buys", new[] { "Client_IdClient" });
            DropIndex("dbo.BuyDetails", new[] { "Vent_IdVent" });
            DropIndex("dbo.BuyDetails", new[] { "Product_IdProduct" });
            DropIndex("dbo.Remboursements", new[] { "Client_IdClient" });
            DropIndex("dbo.Products", new[] { "Emplacer_IdEmplacement" });
            DropIndex("dbo.Products", new[] { "IdCategorie" });
            DropTable("dbo.Buys");
            DropTable("dbo.BuyDetails");
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
