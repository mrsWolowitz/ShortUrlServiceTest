using System;

namespace ShortUrlService.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ShortUrlServiceEntities Init();
    }
}
