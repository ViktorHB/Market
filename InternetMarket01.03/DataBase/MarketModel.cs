namespace DataBase
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MarketModel : DbContext
    {
        public MarketModel()
            : base("data source=viktors.database.windows.net,1433;initial catalog=Data;user id=qwerty;password=1236987Qq;multipleactiveresultsets=True;application name=EntityFramework")
        {
        }

        public virtual DbSet<Access> Access { get; set; }
        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderState> OrderState { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Access>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Access>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Access)
                .HasForeignKey(e => e.id_accessstatud);

            modelBuilder.Entity<Catalog>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog>()
                .HasMany(e => e.Product)
                .WithOptional(e => e.Catalog)
                .HasForeignKey(e => e.id_catalog);

            modelBuilder.Entity<OrderState>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<OrderState>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.OrderState)
                .HasForeignKey(e => e.id_state);

            modelBuilder.Entity<Product>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.id_product);

            modelBuilder.Entity<Users>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.id_user);
        }
    }
}
