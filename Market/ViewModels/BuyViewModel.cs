using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using Market.Models;
using Market.Persistence;

namespace Market.ViewModels
{
    class BuyViewModel : BindableBase
    {
        public ObservableCollection<BuyDetail> BuyDetails { get; set; }
        public ObservableCollection<Buy> Buys { get; set; }

        public BuyViewModel()
        {
            BuyDetails = new ObservableCollection<BuyDetail>();
            Buys = new ObservableCollection<Buy>();

            using (var unitOfWork = new UnitOfWork(new MsContext()))
            {
                foreach (var buy in unitOfWork.BuyRepository.GetAll())
                {
                    
                    Buys.Add(new Buy()
                    {
                        IdBuy = buy.IdBuy,
                        Montant = buy.Montant,
                        DateVent = buy.DateBuy,
                        
                    });


                }
            }

          
        }

        public void BuySelected(Buy buy)
        {
            BuyDetails.Clear();
            using (var unitOfWork = new UnitOfWork(new MsContext()))
            {
                foreach (var buyDetail in unitOfWork.BuyDetailRepository.GetAll())
                {
                    if (buyDetail.IdBuyForeignKey == buy.IdBuy)
                    {
                        string name_product;
                    try
                    {
                        name_product = unitOfWork.Product.Get(buyDetail.IdProductForeignKey).NameProduct;
                    }
                    catch (Exception e)
                    {
                        name_product = "";
                    }

                    
                        BuyDetails.Add(new BuyDetail()
                        {
                            Id = buyDetail.IdBuyDetail,
                            PrixTotal = buyDetail.PrixTotal + "",
                            NameProcuct = name_product,
                            PrixUnit = buyDetail.PrixUnit + "",
                            Qte = buyDetail.Qte + "",
                        });
                    }

                }
            }
            Console.WriteLine("1111111111111111111111111111111111");
        }
    }
}
