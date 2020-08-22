using System;
using Market.Core.Repositories;

namespace Market.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUser User { get; }

        IProduct Product { get; }
        int Complete();
    }
}
