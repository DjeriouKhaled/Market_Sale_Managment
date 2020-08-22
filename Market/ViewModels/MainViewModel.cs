using DevExpress.Xpf.Editors;
using Market.Core.Domain;
using Market.Models;
using Market.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Market.ViewModels
{
    class MainViewModel
    {
        /// Panel centrel : Controls to Binding
        public Image Img { get; set; } = new Image();
        public TextBlock Name_Product { get; set; } = new TextBlock();
        public TextEdit CodeBar { get; set; } = new TextEdit();



        /// <summary>
        ///  view Model for real database 
        /// </summary>
        public List<Product_Model> mProducts { get; private set; } = new List<Product_Model>();
        public List<Categorie_Model> mCategorie_Models { get; set; } = new List<Categorie_Model>();





        /// <summary>
        ///  constracteur
        /// </summary>
        public MainViewModel()
        {
            LoadProductToModel();
            LoadCategorieToModel();


        }
    /// <summary>
    ///  Methodes    ----------------------------------------------------
    /// </summary>
    #region Methodes
    private void LoadProductToModel()
    {
        using (var unitOfWork = new UnitOfWork(new MsContext()))
        {
                if (unitOfWork.Product.GetCount() == 0) return;

                IList<Product_Model> p = new List<Product_Model>();
                var products = unitOfWork.Product.GetAll();
           
                foreach (var product in products)
                {
                    string scat=null;
                    try {
                        scat = unitOfWork.Categorie.Get(product.IdCategorie).NameCategorie; }
                    catch (Exception e) {}

                    BitmapImage image1 = ImageFromBuffer(product.Img);
                    p.Add(new Product_Model() { IdProduct = product.IdProduct, NameProduct = product.NameProduct, Category = scat, Image = image1, Price = product.Prix });
                }

                mProducts.AddRange(p);
           
        }
    }
        public BitmapImage ImageFromBuffer(Byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
        private void LoadCategorieToModel()
    {
        using (var unitOfWork = new UnitOfWork(new MsContext()))
        {
            mCategorie_Models.Add(new Categorie_Model() { Id = 0, NameCategorie = "All" });
            if (unitOfWork.Categorie.GetCount() == 0) return;
            
           foreach (Categorie category in unitOfWork.Categorie.GetAll())
           mCategorie_Models.Add(new Categorie_Model() { Id = category.IdCategorie + 1, NameCategorie = category.NameCategorie });
            
        }
    }
        #endregion

        /// <summary>
        ///  Events    ----------------------------------------------------
        /// </summary>

        #region Events

        // Change Selected Categorie to filter product panel
        public void ChangeSelectedCategorie()
    {
        //   mProducts.RemoveAll(df => true);

        var c = mCategorie_Models.FindAll(s => true);
        c.RemoveAt(0);
        c.Insert(0, new Categorie_Model { Id = 0, NameCategorie = "khaled" });
        mCategorie_Models.AddRange(c);
        //   NotifyFullNameChangedRaisePropertyChanged(nameof(CmboCategories));
        // CmboCategories.RaisePropertiesChanged();
        // MessageBox.Show("Detail info"+mProducts.Count);
    }
        
        public void testMethod(Product_Model p)
        {
            Img.Source = p.Image;
            Name_Product.Text = p.NameProduct;
            // MessageBox.Show(p.IdProduct.ToString());

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
    #endregion Events fin

      
    }
}
