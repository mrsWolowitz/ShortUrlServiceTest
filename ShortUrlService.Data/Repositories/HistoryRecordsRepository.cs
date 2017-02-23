using ShortUrlService.Data.Infrastructure;
using ShortUrlService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlService.Data.Repositories
{
    public class HistoryRecordsRepository : RepositoryBase<HistoryRecord>, IHistoryRecordRepository
    {
        public HistoryRecordsRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IHistoryRecordRepository : IRepository<HistoryRecord>
    {

    }
}
