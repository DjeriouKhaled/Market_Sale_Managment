using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Models
{
    public class BuyDetail
    {
        public int Id { get; set; }

        public int IdBuy_ForeignKey { get; set; }

        public int IdProduct_ForeignKey { get; set; }


        public string NameProcuct { get; set; }

        public string Qte { get; set; }

        public string PrixUnit { get; set; }

        public string PrixTotal { get; set; }
    }
}
