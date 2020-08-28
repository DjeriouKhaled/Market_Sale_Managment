using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Market.Core.Domain
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }

        [Required]
        public string NameProduct { get; set; }

        public byte[] Img { get; set; }

        [Required]
        public int Prix { get; set; }

        public int PrixSale { get; set; }

        public int Qte { get; set; }

        public bool HasBarCode { get; set; }
   
        public string BarCode { get; set; }

        public int QteRisque { get; set; }

        public int Order { get; set; }
        
        public DateTime? DateSale { get; set; }

        public DateTime? DatePeremp { get; set; }

        public bool BuyByPacket { get; set; }

        public int PrixUnitPacket { get; set; }

        public int NumberProductInOnePacket { get; set; }

        public int NumberDayAlerte { get; set; }
        [MaxLength(255)]
        public string SomeNote { get; set; }

        public int? IdCategorie { get; set; }

        [ForeignKey("IdCategorie")]
        public virtual Categorie Categorie { get; set; }

        
       

        public  Emplacer Emplacer { get; set; }
        
    }
}
