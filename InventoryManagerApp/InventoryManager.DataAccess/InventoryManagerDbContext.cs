using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace InventoryManager.DataAccess
{
    public partial class InventoryManagerDbContext : DbContext
    {
        public InventoryManagerDbContext()
        {
        }

        public InventoryManagerDbContext(DbContextOptions<InventoryManagerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProductMaster> ProductMasters { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("server=localhost;database=InventoryDB;Trusted_Connection=true");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<ProductMaster>(entity =>
            {
                entity.ToTable("Product_Master");

                entity.HasIndex(e => e.ItemName, "UQ__Product___A383F4A7F5101B0C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("ITEM_NAME");

                entity.Property(e => e.Price).HasColumnName("PRICE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
