using Microsoft.EntityFrameworkCore;


namespace BuyingAgentBackEnd.Entities
{
    public class BuyingAgentContext : DbContext
    {
        public BuyingAgentContext(DbContextOptions<BuyingAgentContext> options)
            : base(options)
        {
            Database.Migrate();

        }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Visit> Visits { get; set; }

        public DbSet<TransactionProduct> TransactionProducts { get; set; }

        //composit key for the joint table product category

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionProduct>()
                .HasKey(t => new { t.TransactionId, t.ProductId });

            //modelBuilder.Entity<ProductCategory>()
            //    .HasOne(pc => pc.Product)
            //    .WithMany("ProductCategories");

            //modelBuilder.Entity<ProductCategory>()
            //    .HasOne(pc => pc.Category)
            //    .WithMany("ProductCategories");
        }

    }
}