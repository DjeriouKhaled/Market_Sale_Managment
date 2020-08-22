using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using Market.Core.Domain;
using Market.Core.Repositories;


namespace Market.Persistence.Repositories
{
    class ProductRepository : Repository<Product>, IProduct
    {
        public MsContext MsContext => Context as MsContext;
        public ProductRepository(DbContext context) : base(context)
        {
        }

        

        public string GetTheCategorieProduct(Product product)
        {

            MsContext.Entry(product).Reference(s=>s.Categorie).Load();
            MsContext.Entry(product).Reference(s => s.Categorie)
                .Query()
                .Where(sc => sc.IdCategorie == 1);
            return product.Categorie.NameCategorie;
        }

        
    }
}
