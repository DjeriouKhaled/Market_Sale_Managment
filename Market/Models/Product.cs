using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Market.Models
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }

        public string CodeBar { get; set; }

        public int? IdCategory { get; set; }

        public BitmapImage Image { get; set; }

        public int Price_unit { get; set; }

    }
}
