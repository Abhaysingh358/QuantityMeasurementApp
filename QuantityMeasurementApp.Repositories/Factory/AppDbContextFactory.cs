using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using QuantityMeasurementApp.Repositories.Context;

namespace QuantityMeasurementApp.Repositories
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=QuantityMeasurementDB;Trusted_Connection=true;TrustServerCertificate=true;")
                .Options;

            return new AppDbContext(options);
        }
    }
}