using ShortUrlService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlService.Data.Configuration
{
    public class HistoryRecordConfiguration : EntityTypeConfiguration<HistoryRecord>
    {

        public HistoryRecordConfiguration()
        {
            ToTable("HistoryRecords");
            Property(g => g.UrlLong).IsRequired().HasMaxLength(300);
            Property(g => g.UrlShort).HasMaxLength(100);
            Property(g => g.CreateDate).IsRequired();
            //Property(g => g.CreateDate).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(g => g.Id).IsRequired();
        }
    }
}

