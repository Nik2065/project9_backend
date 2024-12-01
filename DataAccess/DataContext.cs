using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataContext : DbContext
    {

        public DataContext(string connectionString = null)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }


        private string? _connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString == null)
                optionsBuilder.UseInMemoryDatabase(databaseName: "Project9db");
            else
                optionsBuilder.UseMySql(
                    _connectionString,
                    new MySqlServerVersion(new Version(8, 0, 11))
                );
        }


        public DbSet<DictionaryForCpuDb> DictionaryForCpuDb { get; set; }
        public DbSet<DictionaryForGpuDb> DictionaryForGpuDb { get; set; }
        public DbSet<ComputerDb> ComputersDb { get; set; }

        public DbSet<SiteUserDb> SiteUsers { get; set; }
        public DbSet<ProductDb> Products { get; set; }


        //private void FillDictionaryForCpuDb()
        //{
        //    var d1 = new DictionaryForCpuDb();

        //    DictionaryForCpuDb.Add(d1);
        //}

    }
}
