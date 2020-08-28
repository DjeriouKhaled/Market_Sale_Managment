using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Core.Domain
{
    [Table("Buy")]
    public class Buy
    {
        [Key, Column("BuyID")]
        public int IdBuy { get; set; }

        public int Montant { get; set; }

        public DateTime DateBuy { get; set; }

     
        public User User { get; set; }

       
        public Client Client { get; set; }
    }
}
