using System;

namespace ShortUrlService.Model
{
    public class HistoryRecord
    {
        public long Id { get; set; }
        public string UrlLong { get; set; }
        public string UrlShort { get; set; }
        public int HitCount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
