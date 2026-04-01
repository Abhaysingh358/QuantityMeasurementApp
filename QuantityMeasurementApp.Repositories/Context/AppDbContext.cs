using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Models.Entities; 
using System.Reflection.Emit;

namespace QuantityMeasurementApp.Repositories.Context
{

    // AppDbContext is the bridge between your C# code and SQL Server.
    // EF Core uses this class to:
    //   1. Know which tables exist  (DbSet<T> properties)
    //   2. Configure column names, constraints, max lengths (OnModelCreating)
    //   3. Run migrations (dotnet ef migrations add ...)

    public class AppDbContext : DbContext
    {
        // This constructor accepts options from outside (connection string etc.)
        // It lets you pass different configs in production vs tests.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // This property = the MeasurementHistory table in SQL Server.
        // EF Core will create / migrate the table to match MeasurementHistoryRecord.
        public DbSet<MeasurementHistoryRecord> MeasurementHistory { get; set; }

        // below users are using for authorisation
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeasurementHistoryRecord>(entity =>
            {
                // Table name in SQL Server
                entity.ToTable("MeasurementHistory");

                // Primary key (EF Core detects "Id" by convention, but being explicit is cleaner)
                entity.HasKey(e => e.Id);

                // Required string columns — set max length to avoid NVARCHAR(MAX) defaults
                entity.Property(e => e.Operation)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Operand1Unit)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Operand1MeasurementType)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.ResultType)
                      .IsRequired()
                      .HasMaxLength(20);

                // Optional string columns
                entity.Property(e => e.Operand2Unit).HasMaxLength(50);
                entity.Property(e => e.Operand2MeasurementType).HasMaxLength(50);
                entity.Property(e => e.ResultQuantityUnit).HasMaxLength(50);
                entity.Property(e => e.ResultQuantityMeasurementType).HasMaxLength(50);
                entity.Property(e => e.ErrorMessage).HasMaxLength(500);

                // CreatedAt default value set by SQL Server when a row is inserted
                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}