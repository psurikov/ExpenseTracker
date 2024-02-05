using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Database
{
    public class DatabaseContext : DbContext
    {
        public IConfiguration Configuration { get; set; }

        public DatabaseContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new SqliteConnectionStringBuilder();
            builder.DataSource = Configuration.GetSection("DatabaseFile").Value;
            var connectionString = builder.ToString();
            optionsBuilder.UseSqlite(connectionString);
        }

        public DbSet<ExpenseRecord> Expenses { get; set; }
        public DbSet<CurrencyRecord> Currencies { get; set; }
        public DbSet<CategoryRecord> Categories { get; set; }
    }
}
