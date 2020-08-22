using System.Data.Entity;
using Market.Core.Domain;
using Market.Core.Repositories;

namespace Market.Persistence.Repositories
{
    class UserRepository : Repository<User>, IUser
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
        public MsContext MsContext => Context as MsContext;
    }
}
