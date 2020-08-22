using System.ComponentModel.DataAnnotations;

namespace Market.Core.Domain
{
    public class Setting
    {
        [Key]
        public int IdSetting { get; set; }
        public string Version { get; set; }

    }
}
