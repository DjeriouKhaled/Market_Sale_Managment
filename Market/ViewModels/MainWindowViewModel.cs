using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;
using Market.Models;
using Market.Persistence;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BuyDetail = Market.Models.BuyDetail;
using Product = Market.Models.Product;

namespace Market.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        public int IdProductCurrentSelection;
        public TextBlock Total { get; set; } = new TextBlock();
        public TextEdit tbPositionCursor { get; set; } = new TextEdit();
        public Image Img { get; set; } = new Image();
        public TextBlock Name_Product { get; set; } = new TextBlock();
        public TextEdit CodeBar { get; set; } = new TextEdit();

        public TextBlock prix_unit_Product { get; set; } = new TextBlock();
        public TextEdit Qte_Product { get; set; } = new TextEdit();
        public TextBlock prix_total_Product { get; set; } = new TextBlock();


        // Collections
        public ObservableCollection<Product> mProducts { get; set; }

        public ObservableCollection<CmboCategorie> CmboCategories { get; set; }

        public ObservableCollection<BuyDetail> BuyDetails { get; set; }



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
                        new Product()
                        {
                            IdProduct = product.IdProduct,
                            NameProduct = product.NameProduct,
                            CodeBar = product.BarCode,
                            IdCategory = product.IdCategorie,
                            Image = ImageFromBuffer(product.Img),
                            Price_unit = product.Prix
                        });
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
                                    Qte_Product.Text = 1 + string.Empty;
                                    prix_total_Product.Text = 1 * Convert.ToInt32(prix_unit_Product.Text) + string.Empty;
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

            if (tbPositionCursor.Text != string.Empty)
            {
                BuyDetails.Add(new BuyDetail()
                {
                    Id = id_buy_detail,
                    PrixTotal = tbPositionCursor.Text,
                });
                if (Total.Text != null)
                {
                    Total.Text = Convert.ToInt32(Total.Text) + Convert.ToInt32(tbPositionCursor.Text) + string.Empty;
                }
                tbPositionCursor.Text = string.Empty;
                id_buy_detail++;
            }

            else
            {
                if (Name_Product.Text != string.Empty)
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
                        Total.Text = Convert.ToInt32(Total.Text) + Convert.ToInt32(prix_total_Product.Text) + string.Empty;
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
            Qte_Product.Text = 1 + string.Empty;
            prix_total_Product.Text = 1 * Convert.ToInt32(prix_unit_Product.Text) + string.Empty;
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
            tbPositionCursor.Text = string.Empty;
            BuyDetails.Clear();
        }
        public void Delete_Buy_OnItemClick()
        {
            id_buy_detail = 1;
            ClearProductSelected();
            Total.Text = "0";
            tbPositionCursor.Text = string.Empty;
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
                    prix_total_Product.Text = Convert.ToInt32(Qte_Product.Text) * Convert.ToInt32(prix_unit_Product.Text) + string.Empty;
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
