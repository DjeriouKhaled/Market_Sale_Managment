using System;
using Market.Core;
using Market.Core.Domain;
using Market.Core.Repositories;
using Market.Persistence.Repositories;

namespace Market.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MsContext _context;
       

      
        public ISetting Setting { get; }

        public IProduct Product { get; }
        public IUser  User { get; }
        public IRepository<Buy> BuyRepository { get; }
        public IRepository<BuyDetail> BuyDetailRepository { get; }
        public IRepository<Categorie> Categorie { get; }
    

     

        public UnitOfWork(MsContext context)
        {
            _context = context;
            BuyRepository = new Repository<Buy>(_context);
            Setting = new SettingRepo(_context);
            BuyDetailRepository = new Repository<BuyDetail>(_context);
            Categorie = new Repository<Categorie>(_context);
            User = new UserRepository(_context);
            Product = new ProductRepository(_context);
        }

        public int Complete() { return _context.SaveChanges(); }

        public void Dispose() { _context.Dispose(); }
    }
}
