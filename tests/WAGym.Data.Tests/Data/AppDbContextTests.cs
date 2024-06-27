using Microsoft.EntityFrameworkCore;
using WAGym.Common.Tests.Mock;
using WAGym.Data.Data;

namespace WAGym.Data.Tests.Data
{
    public class AppDbContextTests : AppDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "WAGym");
        }

        public DbSet<MockClass> MockClass { get; set; }
    }
}
