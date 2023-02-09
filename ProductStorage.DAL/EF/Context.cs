using Microsoft.EntityFrameworkCore;
using ProductStorage.DAL.Entities;

namespace ProductStorage.DAL.EF
{
    public class Context : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerProduct> CustomerProducts { get; set; }

        public Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=HOKAIDO;Initial Catalog=ProductStorage;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // using Fluent API
        {
            
            modelBuilder
                .Entity<Customer>()
                .HasMany(p => p.Products)
                .WithMany(c => c.Customers)
                .UsingEntity<CustomerProduct>(
                   j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(t => t.CustomerProducts)
                    .HasForeignKey(pt => pt.ProductId),
                j => j
                    .HasOne(pt => pt.Customer)
                    .WithMany(p => p.CustomerProducts)
                    .HasForeignKey(pt => pt.CustomerId),
                j =>
                {
                    j.HasKey(t => new { t.ProductId, t.CustomerId });
                    j.Property(pt => pt.Amount).HasDefaultValue(0);
                    j.ToTable("CustomersProducts");
                });         

        }

    }
}
