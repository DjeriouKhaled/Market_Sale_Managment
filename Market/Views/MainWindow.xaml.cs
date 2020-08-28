using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using Market.Core.Domain;
using Market.Persistence;
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
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm.Native;

namespace Market.Views
{
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
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

  

        private void Btn_false_OnClick(object sender, RoutedEventArgs e)
        {

            if (Qt_TextEdit.IsKeyboardFocusWithin)
            {
                if (Qt_TextEdit.Text.Length!=0)
                {
                    Qt_TextEdit.Text = Qt_TextEdit.Text.Remove(Qt_TextEdit.Text.Length - 1);
                }
            }
            else if (CodeBar.IsKeyboardFocusWithin)
            {
                if (CodeBar.Text.Length != 0)
                {
                    CodeBar.Text = CodeBar.Text.Remove(CodeBar.Text.Length - 1);
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
            else if(CodeBar.IsKeyboardFocusWithin)
            {
                CodeBar.Text = CodeBar.Text + s;
                // MakeIndexInLast
                CodeBar.Text = CodeBar.Text.Trim();
                CodeBar.Select(CodeBar.Text.Length, 0);
            }
            else
            {
                tbPositionCursor.Text = tbPositionCursor.Text + s;
                // MakeIndexInLast
                tbPositionCursor.Select(tbPositionCursor.Text.Length, 0);
            }
        }


        private void AboutAPP_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var cApp = ((App)Application.Current);
            cApp.MainWindow = new AboutApp();
            cApp.MainWindow.ShowDialog();
        }
    }

    #region Models

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


    public class Buy
    {
        public int IdBuy { get; set; }

        public int Montant { get; set; }

        public DateTime DateVent { get; set; }
    }

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
       
        // 
        public int IdProductCurrentSelection;
        public TextBlock Total { get; set; } = new TextBlock();
        public TextEdit tbPositionCursor { get; set; } = new TextEdit();
        public System.Windows.Controls.Image Img { get; set; }=new System.Windows.Controls.Image();
        public  TextBlock Name_Product { get; set; }=new TextBlock();
        public  TextEdit CodeBar { get; set; }=new TextEdit();

        public TextBlock prix_unit_Product { get; set; } = new TextBlock();
        public TextEdit Qte_Product { get; set; } = new TextEdit();
        public TextBlock prix_total_Product { get; set; } = new TextBlock();

    
        // Collections
        public ObservableCollection<Product> mProducts { get; set; } 
        
        public ObservableCollection<CmboCategorie> CmboCategories { get; set; }

        public ObservableCollection<BuyDetail> BuyDetails { get; set; }
        
    

        /// <summary>
        ///  constracteur for ViewModel principal
        /// </summary>
        public MainWindowViewModel()
        {
            CmboCategories = new ObservableCollection<CmboCategorie>();
            mProducts = new ObservableCollection<Product>();
            BuyDetails = new ObservableCollection<BuyDetail>();
            Total.Text = "0";

            using (var unitOfWork = new UnitOfWork(new MsContext()))
            {
                // TODO :
                 
                    foreach (var product in unitOfWork.Product.GetAll())
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
                   
                    foreach (var category in unitOfWork.Categorie.GetAll())
                    {
                        CmboCategories.Add(new CmboCategorie()
                        {
                            Id = category.IdCategorie,
                            NameCategorie = category.NameCategorie
                        });

                    }
                
            }


         


    }


        public void SearchProductWithCodeBar()
        {
            if (CodeBar.EditValue == null)
            {
                Img.Source = new BitmapImage();
                Name_Product.Text = string.Empty;
                prix_unit_Product.Text = string.Empty;
                Qte_Product.Text = string.Empty;
                prix_total_Product.Text = string.Empty;
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
                                    Img.Source = ImageFromBuffer(product.Img);
                                    prix_unit_Product.Text = product.Prix.ToString();
                                    Qte_Product.Text = 1 + "";
                                    prix_total_Product.Text = 1 * Convert.ToInt32(prix_unit_Product.Text) + "";
                                    break;
                                }
                                else
                                {
                                    Img.Source = new BitmapImage();
                                    Name_Product.Text = string.Empty;
                                    prix_unit_Product.Text = string.Empty;
                                    Qte_Product.Text = string.Empty;
                                    prix_total_Product.Text = string.Empty;
                                }
                                   
                            }

                        }
                    }

                }
            }
            else
            {
                Img.Source = new BitmapImage();
                Name_Product.Text = string.Empty;
                prix_unit_Product.Text = string.Empty;
                Qte_Product.Text = string.Empty;
                prix_total_Product.Text = string.Empty;

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

        public int id_buy_detail = 1;
        public void Btn_validate_OnClick()
        {
            
                if (tbPositionCursor.Text!="")
                {
                    BuyDetails.Add(new BuyDetail()
                    {
                        Id = id_buy_detail,
                        PrixTotal = tbPositionCursor.Text,
                    });
                    if (Total.Text != null)
                    {
                        Total.Text = Convert.ToInt32(Total.Text) + Convert.ToInt32(tbPositionCursor.Text) + "";
                    }
                    tbPositionCursor.Text = "";
                    id_buy_detail++;
                }

                else
                {
                    if (Name_Product.Text != "")
                    {
                        BuyDetails.Add(new BuyDetail()
                        {
                            Id = id_buy_detail,
                            NameProcuct = Name_Product.Text,
                            PrixTotal = prix_total_Product.Text,
                            PrixUnit = prix_unit_Product.Text,
                            Qte = Qte_Product.Text,
                            IdProduct_ForeignKey = IdProductCurrentSelection,

                        });
                        if (Total.Text != null)
                        {
                            Total.Text = Convert.ToInt32(Total.Text) + Convert.ToInt32(prix_total_Product.Text) + "";
                        }
                        ClearProductSelected();
                        id_buy_detail++;
                    }
                }
                            
         
            
        
               
            
           

        }

      

        public void ProductSelected(Product p)
        {
            Img.Source = p.Image;
            Name_Product.Text = p.NameProduct;
            prix_unit_Product.Text = p.Price_unit.ToString();
            CodeBar.Text = p.CodeBar;
            Qte_Product.Text = 1+"";
            prix_total_Product.Text = 1 * Convert.ToInt32(prix_unit_Product.Text) + "";
            IdProductCurrentSelection = p.IdProduct;
            // MessageBox.Show(p.IdProduct.ToString());
        }


        public void New_Buy_OnItemClick()
        {
            int buyId;
           
            using (var U = new UnitOfWork(new MsContext()))
            {
                Core.Domain.Buy buy2 = new Core.Domain.Buy()
                {
                    DateBuy = DateTime.Now,
                    Montant = Convert.ToInt32(Total.Text),

                };
                U.BuyRepository.Add(buy2);
                U.Complete();

                buyId = buy2.IdBuy;

                foreach (var buyDetail in BuyDetails)
                {
                    var buy_detail = new Core.Domain.BuyDetail()
                    {
                        IdBuyDetail = buyDetail.Id,
                        PrixUnit = Convert.ToInt32(buyDetail.PrixUnit),
                        PrixTotal = Convert.ToInt32(buyDetail.PrixTotal),
                        Qte = Convert.ToInt32(buyDetail.Qte),
                        IdBuyForeignKey = buyId,
                        IdProductForeignKey = buyDetail.IdProduct_ForeignKey,

                    };
                    U.BuyDetailRepository.Add(buy_detail);
                }
                
                U.Complete();
            }
   
        
            ClearProductSelected();
            Total.Text = "0";
            tbPositionCursor.Text = "";
            BuyDetails.Clear();
        }
        public void Delete_Buy_OnItemClick()
        {
            id_buy_detail = 1;
            ClearProductSelected();
            Total.Text = "0";
            tbPositionCursor.Text = "";
            BuyDetails.Clear();
        }




        // help methods ClearProductSeected
        public void ClearProductSelected()
        {
            IdProductCurrentSelection = 0;
            Img.Source = new BitmapImage();
            Name_Product.Text = string.Empty;
            prix_unit_Product.Text = string.Empty;
            CodeBar.Text = string.Empty;
            Qte_Product.Text = string.Empty;
            prix_total_Product.Text = string.Empty;
        }
        
        public BitmapImage ImageFromBuffer(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }





        public void Qte_ProductChanged()
        {

            try
            {
                if (Qte_Product.Text != null)
                {
                    prix_total_Product.Text = Convert.ToInt32(Qte_Product.Text) * Convert.ToInt32(prix_unit_Product.Text) + "";
                }
            }
            catch (Exception e)
            {
                prix_total_Product.Text = prix_unit_Product.Text;

                Console.WriteLine(e);
            }

        }
//
//        public bool is_tbPositionCursor_focus;
//        public void tbPositionCursor_LostKeyboardFocus()
//        {
//            is_tbPositionCursor_focus = false;
//            Console.WriteLine("tbPositionCursor_LostKeyboardFocus" + is_tbPositionCursor_focus);
//        }
//
//        public void tbPositionCursor_GotKeyboardFocus()
//        {
//            is_tbPositionCursor_focus = true;
//        }
//
//        public void TbPositionCursor_OnEditValueChanged()
//        {
//            //            tbPositionCursor.Text = tbPositionCursor.Text.Trim();
//            //            tbPositionCursor.Select(tbPositionCursor.Text.Length, 0);
//        }
    }
}
