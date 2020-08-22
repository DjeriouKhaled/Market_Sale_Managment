using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Core.Domain
{
    public class Remboursement
    {       
        [Key]
        public DateTime DateRemb { get; set; }
     
        public int Value { get; set; }
      
        public string Observ { get; set; }

       
        public Client Client { get; set; }



    }
}
