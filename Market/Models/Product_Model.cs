using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Market.Models
{
    public class Product_Model
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }

        public string Category { get; set; }

        public BitmapImage Image { get; set; }

        public int Price { get; set; }
    }
}
