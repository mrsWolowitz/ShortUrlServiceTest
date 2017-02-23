using ShortUrlService.Data.Configuration;
using ShortUrlService.Model;
using System.Data.Entity;

namespace ShortUrlService.Data
{
    public class ShortUrlServiceEntities : DbContext
    {
        public ShortUrlServiceEntities() : base("ShortUrlServiceEntities") { }

        public DbSet<HistoryRecord> HistoryRecords { get; set; }
  
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new HistoryRecordConfiguration());
        }
              
    }
}
