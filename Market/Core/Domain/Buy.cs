using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Core.Domain
{
    public class Buy
    {
        [Key]
        public int IdVent { get; set; }

        public int Montant { get; set; }

        public DateTime DateVent { get; set; }

     
        public User User { get; set; }

       
        public Client Client { get; set; }



    }
}
