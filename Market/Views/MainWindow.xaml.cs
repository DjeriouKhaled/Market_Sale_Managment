using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using Market.Core.Domain;
using Market.Persistence;
using Market.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Market.Views
{
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
          
            using (var U1 =new UnitOfWork(new MsContext()))
            {
                if (U1.Product.GetCount()!=0)
                {
                    foreach (Core.Domain.Product user in U1.Product.GetAll())
                    {
                        U1.Categorie.Get(-1);

                        Console.WriteLine(user.NameProduct + " " + user.IdProduct + " " + U1.Categorie.Get(user.IdCategorie).NameCategorie + " " + user.Prix);
                    }
                }
            }

           
        }

        private static bool IsTextAllowed(string text)
        {
            
            Regex regex = new Regex("\\D"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void bobo(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TbPositionCursor_OnEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            tbPositionCursor.Text = tbPositionCursor.Text.Trim();
            tbPositionCursor.Select(tbPositionCursor.Text.Length , 0);
            
         //   GridLength g =new GridLength(BLayoutPanel.ItemHeight.Value+0.11,GridUnitType.Star);
         //   BLayoutPanel.ItemHeight = g;
        }

        private void Btn_false_OnClick(object sender, RoutedEventArgs e)
        {

            if (Qt_TextEdit.IsKeyboardFocusWithin)
            {
                if (Qt_TextEdit.Text.Length!=0)
                {
                    Qt_TextEdit.Text = Qt_TextEdit.Text.Remove(Qt_TextEdit.Text.Length - 1);
                }
            }
            else
            {
                if (tbPositionCursor.Text.Length != 0)
                {
                    tbPositionCursor.Text = tbPositionCursor.Text.Remove(tbPositionCursor.Text.Length - 1);
                }
               
            }
        }
        private void Btn_validate_OnClick(object sender, RoutedEventArgs e)
        {
            if (Qt_TextEdit.IsKeyboardFocusWithin)
            {
               
            }
            else
            {     
                    tbPositionCursor.Text ="";
                

            }
        }

        private void Btn_0_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("0"); }
        private void Btn_1_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("1"); }
        private void Btn_2_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("2"); }
        private void Btn_3_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("3"); }
        private void Btn_4_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("4"); }
        private void Btn_5_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("5"); }
        private void Btn_6_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("6"); }
        private void Btn_7_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("7"); }
        private void Btn_8_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("8"); }       
        private void Btn_9_OnClick(object sender, RoutedEventArgs e) { AddNumberToEditText("9");}


        private void AddNumberToEditText(string s)
        {
            if (Qt_TextEdit.IsKeyboardFocusWithin)
            {
                Qt_TextEdit.Text = Qt_TextEdit.Text + s;
                // MakeIndexInLast
                Qt_TextEdit.Text = Qt_TextEdit.Text.Trim();
                Qt_TextEdit.Select(Qt_TextEdit.Text.Length, 0);
            }
            else
            {
                tbPositionCursor.Text = tbPositionCursor.Text + s;
                // MakeIndexInLast
                tbPositionCursor.Select(tbPositionCursor.Text.Length, 0);
            }
        }

        public  void ImagePinc( )
        {
           // ImagePrincipal.Source=
        }

        private void BarItem_RefreshProducts(object sender, ItemClickEventArgs e)
        {
            
            BorderCalculator.Visibility = BorderCalculator.Visibility.Equals(Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }



    }

    #region Models

    public class CmboCategorie : INotifyPropertyChanged
    {
        private int id;
        private string nameCategorie;
      

        public int Id { get { return id; }
                    set { id = value; OnPropertyChanged("Id"); } }
    
        public string NameCategorie
        {
            get { return nameCategorie; }
            set { nameCategorie = value; OnPropertyChanged("NameCategorie"); }
        }

        private void OnPropertyChanged(string s)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            ph?.Invoke(this, new PropertyChangedEventArgs(s));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Customer
    {
        public string Name { get; set; }

        public string City { get; set; }

        public int Visits { get; set; }

        public DateTime? Birthday { get; set; }
    }

    public class Product
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }

        public string CodeBar { get; set; }

        public int? IdCategory { get; set; }

        public BitmapImage Image { get; set; }

        public int Price_unit { get; set; }

    }

    #endregion

    public class MainWindowViewModel : BindableBase
    {

        public ClientViewModel ClientVM;
        // solo
        public System.Windows.Controls.Image Img { get; set; }=new System.Windows.Controls.Image();
        public  TextBlock Name_Product { get; set; }=new TextBlock();
        public  TextEdit CodeBar { get; set; }=new TextEdit();

        public TextBlock prix_unit_Product { get; set; } = new TextBlock();

        // Collections
        public ObservableCollection<Product> mProducts { get; private set; } 
        
        public ObservableCollection<CmboCategorie> CmboCategories { get; private set; }
        
        //  fake data
        public List<Customer> Customers { get; private set; }
        public List<string> Cities { get; private set; }



        public BitmapImage ImageFromBuffer(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
        /// <summary>
        ///  constracteur for ViewModel principal
        /// </summary>
        public MainWindowViewModel()
        {
            CmboCategories = new ObservableCollection<CmboCategorie>();
            mProducts = new ObservableCollection<Product>();
            // ClientVM = new ClientViewModel(this);

            #region Costomer List fake data
            var people = new List<Customer>
            {
                new Customer()
                { Name = "Gregory S. Price", City = "Hong Kong", Visits = 4, Birthday = new DateTime(1980, 1, 1) },
                new Customer()
                { Name = "Irma R. Marshall", City = "Madrid", Visits = 2, Birthday = new DateTime(1966, 4, 15) },
                new Customer()
                { Name = "John C. Powell", City = "Los Angeles", Visits = 6, Birthday = new DateTime(1982, 3, 11) },
                new Customer()
                { Name = "Christian P. Laclair", City = "London", Visits = 11, Birthday = new DateTime(1977, 12, 5) },
                new Customer()
                { Name = "Karen J. Kelly", City = "Hong Kong", Visits = 8, Birthday = new DateTime(1956, 9, 5) },
                new Customer()
                { Name = "Brian C. Cowling", City = "Los Angeles", Visits = 5, Birthday = new DateTime(1990, 2, 27) },
                new Customer()
                { Name = "Thomas C. Dawson", City = "Madrid", Visits = 21, Birthday = new DateTime(1965, 5, 5) },
                new Customer()
                { Name = "Angel M. Wilson", City = "Los Angeles", Visits = 8, Birthday = new DateTime(1987, 11, 9) },
                new Customer()
                { Name = "Winston C. Smith", City = "London", Visits = 1, Birthday = new DateTime(1949, 6, 18) },
                new Customer()
                { Name = "Harold S. Brandes", City = "Bangkok", Visits = 3, Birthday = new DateTime(1989, 1, 8) },
                new Customer()
                { Name = "Michael S. Blevins", City = "Hong Kong", Visits = 4, Birthday = new DateTime(1972, 9, 14) },
                new Customer()
                { Name = "Jan K. Sisk", City = "Bangkok", Visits = 6, Birthday = new DateTime(1989, 5, 7) },
                new Customer()
                { Name = "Sidney L. Holder", City = "London", Visits = 19, Birthday = new DateTime(1971, 10, 3) }
            };
            Customers = people;

            var cities = from c in people select c.City;
            Cities = cities.Distinct().ToList();
            #endregion

            

            using (var unitOfWork = new UnitOfWork(new MsContext()))
            {
                // TODO : 
                var products = unitOfWork.Product.GetAll();
                if (products!=null)
                {
                    foreach (var product in products)
                    {
                        mProducts.Add(
                            new Product() {
                                  IdProduct = product.IdProduct,
                                  NameProduct = product.NameProduct,
                                  CodeBar=product.BarCode,
                                  IdCategory = product.IdCategorie,
                                  Image = ImageFromBuffer(product.Img),
                                  Price_unit = product.Prix });
                    }
                    
                }

               

                var categories = unitOfWork.Categorie.GetAll();
                CmboCategories.Add(new CmboCategorie() { Id = -1, NameCategorie = "All" });
             
                if (categories!=null)
                {
                    foreach (Categorie category in categories)
                    {
                        CmboCategories.Add(new CmboCategorie() {
                                                Id = category.IdCategorie,
                                                NameCategorie = category.NameCategorie});
                      
                    }
                }
              
            }


         


    }


        public void SearchProductWithCodeBar()
        {
            if (CodeBar.EditValue == null)
            {
                Img.Source = new BitmapImage();
                Name_Product.Text = "";

                return;
            }
            string codebar = CodeBar.EditValue.ToString();

            if (codebar.Length > 2)
            {
                using (var U1 = new UnitOfWork(new MsContext()))
                {
                    if (U1.Product.GetCount() != 0)
                    {
                        foreach (Core.Domain.Product product in U1.Product.GetAll())
                        {
                            if (product.BarCode != null)
                            {
                                if (codebar.Equals(product.BarCode))
                                {
                                    Name_Product.Text = product.NameProduct;
                                    BitmapImage image1 = ImageFromBuffer(product.Img);

                                    Img.Source = image1;

                                    break;
                                }
                                else
                                {
                                    BitmapImage image1 = new BitmapImage();
                                    Name_Product.Text = "";
                                    Img.Source = image1;
                                }
                            }

                        }
                    }

                }
            }
            else
            {
                BitmapImage image1 = new BitmapImage();
                Name_Product.Text = "";
                Img.Source = image1;

            }


        }
        public void ChangeSelectedCategorie()
        {
            //   mProducts.RemoveAll(df => true);


          //  CmboCategorie cmbo = new CmboCategorie() { Id = 7, NameCategorie = "jscsma" };
           // CmboCategories.Add(cmbo);
            //   NotifyFullNameChangedRaisePropertyChanged(nameof(CmboCategories));
            // CmboCategories.RaisePropertiesChanged();
            // MessageBox.Show("Detail info"+mProducts.Count);
        }



        public void testMethod(Product p)
        {
            Img.Source = p.Image;
            Name_Product.Text = p.NameProduct;
            prix_unit_Product.Text = p.Price_unit.ToString();
            // MessageBox.Show(p.IdProduct.ToString());

        }
    }
}
