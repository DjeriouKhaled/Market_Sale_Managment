using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Core.Domain
{
    public class Categorie
    {
        [Key]
        public int IdCategorie { get; set; }

        public string NameCategorie { get; set; }

        public int Order { get; set; }

//        public int? IdProduct { get; set; }
//
//        [ForeignKey("IdProduct")]
//        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

       
       
      
    }
}
