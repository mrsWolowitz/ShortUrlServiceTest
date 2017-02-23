using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlService.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        ShortUrlServiceEntities dbContext;

        public ShortUrlServiceEntities Init()
        {
            return dbContext ?? (dbContext = new ShortUrlServiceEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
