using System;
using System.Linq;
using System.Windows;

namespace Market.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1
    {
        public Window1()
        {
            InitializeComponent();
            var cApp = ((App)Application.Current);
            cApp.MainWindow = new MainWindow();
            cApp.MainWindow.Show();
            Close();
        }
    }
}
