using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market.Core.Domain;
using Market.Core.Repositories;

namespace Market.Persistence.Repositories
{
    class SettingRepo : Repository<Setting>, ISetting
    {
        public MsContext MsContext => Context as MsContext;
        public SettingRepo(DbContext context) : base(context)
        {
        }

        public void change_last_buy(int last_buy)
        {
//            var setting = MsContext.Settings.Create();
//            setting.IdSetting = 1;
//            MsContext.Settings.Attach(setting);
//            setting.IdLastBuy = last_buy;
        }
    }
}
