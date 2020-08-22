using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market.Core.Domain;

namespace Market.Core.Repositories
{
    public interface IProduct : IRepository<Product>
    {
        string GetTheCategorieProduct(Product p);


    }
}
