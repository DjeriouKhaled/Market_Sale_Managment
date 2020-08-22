using System.Collections.Generic;
using Market.Core.Domain;

namespace Market.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Drawing;
    using System.IO;
    using System.Windows.Media.Imaging;

    internal sealed class Configuration : DbMigrationsConfiguration<Persistence.MsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Persistence.MsContext context)
        {
            #region Add User
            var usersList = new List<User>
            {
                new User{ NameUser = "Mosh Hamedani",Password = "1234"},
                new User{ NameUser = "khaled djeriou",Password = "****"},
                new User{ NameUser = "kokach Ibrahim",Password = "****"},
                new User{ NameUser = "ben azi",Password = "****"}
            };
            foreach (var user in usersList)
                context.Users.AddOrUpdate(a => a.IdUser, user);
            #endregion

            #region Categorie
            var categorieDictionary = new Dictionary<string, Categorie>
            {
                {"parfa", new Categorie{ NameCategorie = "parfa" ,Order = 1}},
                {"mawad tandif", new Categorie{  NameCategorie = "mawad tandif" ,Order = 2}}
            };

            foreach (var categorieDictionaryValue in categorieDictionary.Values)
                context.Categories.AddOrUpdate(t => t.IdCategorie, categorieDictionaryValue);
            #endregion

            #region Add Product
            BitmapImage image1 = new BitmapImage(new Uri("E:\\Adobe\\Projects\\xd\\MM\\1.jpg", UriKind.Absolute));
            BitmapImage image2 = new BitmapImage(new Uri("E:\\Adobe\\Projects\\xd\\MM\\2.jpg", UriKind.Absolute));
            BitmapImage image3 = new BitmapImage(new Uri("E:\\Adobe\\Projects\\xd\\MM\\3.jpg", UriKind.Absolute));
            BitmapImage image4 = new BitmapImage(new Uri("E:\\Adobe\\Projects\\xd\\MM\\4.png", UriKind.Absolute));
            BitmapImage image5 = new BitmapImage(new Uri("E:\\Adobe\\Projects\\xd\\MM\\5.png", UriKind.Absolute));
            BitmapImage image6 = new BitmapImage(new Uri("E:\\Adobe\\Projects\\xd\\MM\\6.jpg", UriKind.Absolute));
            BitmapImage image8 = new BitmapImage(new Uri("E:\\Adobe\\Projects\\xd\\MM\\8.jpg", UriKind.Absolute));



            var productList = new List<Product>
            {
                new Product { NameProduct ="Pure-XS" , IdCategorie = 1, Prix = 120,Img =ImageToByteArray(Bitmap_To_Image(image1)) },
                new Product { NameProduct ="Miss Dior" , IdCategorie = 2, Prix = 12,Img =ImageToByteArray(Bitmap_To_Image(image2))},
                new Product { NameProduct ="Dolce & Gabbana" ,IdCategorie = 1, Prix = 3847,Img = ImageToByteArray(Bitmap_To_Image(image3))},
                new Product { NameProduct ="Chanel Chance " , IdCategorie = 2, Prix = 21,Img = ImageToByteArray(Bitmap_To_Image(image4))},
                new Product { NameProduct ="Absolutely Blooming 5" , IdCategorie = 2 , Prix = 834,Img = ImageToByteArray(Bitmap_To_Image(image5))},
                new Product { NameProduct ="BLEU DE CHANEL" ,IdCategorie = 1, Prix = 3847,Img = ImageToByteArray(Bitmap_To_Image(image6))},
                new Product { NameProduct ="ARMANI 9" , IdCategorie = 1 , Prix = 834,Img = ImageToByteArray(Bitmap_To_Image(image8))},


            };
            foreach (var product in productList)
                context.Products.AddOrUpdate(a => a.IdProduct, product);
            #endregion
        }



        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                return ms.ToArray();
            }
        }
        /*
        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                var returnImage = Image.FromStream(ms);

                return returnImage;
            }
        }
        */
        public Image Bitmap_To_Image(BitmapImage bit_image)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapImage)bit_image));
            var stream = new MemoryStream();
            encoder.Save(stream);
            stream.Flush();
            var image = new System.Drawing.Bitmap(stream);
            return image;
        }
    }
}
