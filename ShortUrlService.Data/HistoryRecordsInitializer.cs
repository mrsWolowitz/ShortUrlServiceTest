using System;
using System.Collections.Generic;
using ShortUrlService.Model;

namespace ShortUrlService.Data
{
    public class HistoryRecordsInitializer : System.Data.Entity.CreateDatabaseIfNotExists<ShortUrlServiceEntities>
    {
        protected override void Seed(ShortUrlServiceEntities context)
        {
            var historyRecords = new List<HistoryRecord>
            {
                new HistoryRecord{UrlLong="тест",UrlShort="тест",CreateDate=DateTime.Parse("2017-02-20"), HitCount = 0},
                //new HistoryRecord{UrlLong="https://www.w3schools.com/bootstrap/bootstrap_tables.asp",UrlShort="Alonso",CreateDate=DateTime.Parse("2017-02-20"), HitCount = 3},
                //new HistoryRecord{UrlLong="https://www.w3schools.com/bootstrap/bootstrap_tables.asp",UrlShort="Anand",CreateDate=DateTime.Parse("2017-02-20"), HitCount = 0}
            };

            historyRecords.ForEach(s => context.HistoryRecords.Add(s));
            context.Commit();
        }
    }
}