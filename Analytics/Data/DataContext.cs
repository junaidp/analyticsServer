using Microsoft.EntityFrameworkCore;

namespace Analytics.Data
{
    public class DataContext : DbContext
    {
      public DataContext(DbContextOptions<DataContext> options) : base(options) { }

      public DbSet<AnalyticsModal> Analytics { get; set; }  

      public DbSet<ColumnsModal> Columns { get; set; }
    }
}
