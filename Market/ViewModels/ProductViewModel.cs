using DevExpress.Mvvm;

using System.Windows;

using System.Windows.Input;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Editors;

namespace Market.ViewModels
{
    class ProductViewModel : BindableBase
    {
        public Image Img { get; set; } = new Image();
        public TextBlock Name_Product { get; set; } = new TextBlock();
        public TextEdit CodeBar { get; set; } = new TextEdit();

    }
}
