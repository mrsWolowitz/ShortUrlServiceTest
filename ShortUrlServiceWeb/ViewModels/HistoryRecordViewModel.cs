using System;

namespace ShortUrlServiceWeb.ViewModels
{
    public class HistoryRecordViewModel
    {
        public int ID { get; set; }
        public string UrlLong { get; set; }
        public string UrlShort { get; set; }
        public int HitCount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}