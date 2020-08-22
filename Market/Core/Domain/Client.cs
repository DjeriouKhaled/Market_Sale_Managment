using System;
using System.ComponentModel.DataAnnotations;

namespace Market.Core.Domain
{
     public class Client
    {
        [Key]
        public int IdClient { get; set; }

        public string NameClient { get; set; }

        public int Credit { get; set; }

        public int Status { get; set; }

        public string Observ { get; set; }

        public DateTime DateLastCredit { get; set; }

        public DateTime DateLastRemb { get; set; }

     

    }
}
