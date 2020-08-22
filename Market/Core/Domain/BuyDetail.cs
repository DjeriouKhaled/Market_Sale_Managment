
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Core.Domain
{
    public class BuyDetail
    {
        [Key]
        [Column(Order = 1)]
        public int IdVentDetail { get; set; }

        [Key]
        [Column(Order = 2)]
        public int IdVent_ForeignKey { get; set; }
        
    
        public int IdProduct_ForeignKey { get; set; }
 


        public int Qte { get; set; }
       
        public int PrixUnit { get; set; }
       
        public int PrixTotal { get; set; }



        public Product Product { get; set; }
        public Buy Vent { get; set; }


    }
}
