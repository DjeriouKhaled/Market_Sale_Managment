using System.ComponentModel.DataAnnotations;

namespace Market.Core.Domain
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        [Required,MaxLength(50)]
        public string NameUser { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }
    }
}
