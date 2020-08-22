using System.ComponentModel.DataAnnotations;

namespace Market.Core.Domain
{
    public class Emplacer
    {
        [Key]
        public int IdEmplacement { get; set; }

        public string Emplacement { get; set; }

       
    }
}
