using Market.Core.Domain;
using System;
using System.Linq;

namespace Market.Core.Repositories
{
    public interface ISetting : IRepository<Setting>
    {
        void change_last_buy(int last_buy);
    }
}
