
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Core.Domain
{
    [Table("BuyDetail")]
    public class BuyDetail
    {
        
        [Key,Column("BuyDetailID", Order = 1)]
        public int IdBuyDetail { get; set; }

        [Key]
        [Column(Order = 2)]
        public int IdBuyForeignKey { get; set; }
    
        public int? IdProductForeignKey { get; set; }
 
        public int? Qte { get; set; }
       
        public int? PrixUnit { get; set; }
       
 
        public int PrixTotal { get; set; }

        


    }
}
