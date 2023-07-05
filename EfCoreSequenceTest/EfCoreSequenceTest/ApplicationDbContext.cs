#nullable disable

using EfCoreSequenceTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSequenceTest
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _assemblyName;
        private const string _connectionString = "Server=.\\SQLEXPRESS;Database=Test;Trusted_Connection=True;TrustServerCertificate=True";

        public ApplicationDbContext()
        {
            _assemblyName = typeof(Program).Assembly.FullName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString, m => m.MigrationsAssembly(_assemblyName));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence("EmployeeNumber")
                .StartsAt(500)
                .IncrementsBy(1);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeId)
                .HasDefaultValueSql("NEXT VALUE FOR EmployeeNumber");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
