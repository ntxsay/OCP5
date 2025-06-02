using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OCP5.Models.Entities;

namespace OCP5.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private IDbConnection DbConnection { get; }

        
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Repairing> Repairings { get; set; }
        public DbSet<Finition> Finitions { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<VehicleYear> VehicleYears { get; set; }
        public DbSet<PriceMargin> PriceMargins { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
            : base(options)
        {
            DbConnection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConnection.ConnectionString, providerOptions => providerOptions.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //marge de prix
            modelBuilder.Entity<PriceMargin>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<PriceMargin>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<PriceMargin>()
                .Property(x => x.Price)
                .IsRequired();
            modelBuilder.Entity<PriceMargin>()
                .HasIndex(x => x.Price)
                .IsUnique();

            //Année véhicules
            modelBuilder.Entity<VehicleYear>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<VehicleYear>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<VehicleYear>()
                .Property(x => x.Year)
                .IsRequired();
            modelBuilder.Entity<VehicleYear>()
                .HasIndex(x => x.Year)
                .IsUnique();


            //Model
            modelBuilder.Entity<Model>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Model>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Model>()
                .Property(x => x.Name)                
                .HasMaxLength(255)
                .IsRequired();
            
            //Brand
            modelBuilder.Entity<Brand>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Brand>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Brand>()
                .Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
            modelBuilder.Entity<Brand>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Brand>()
                .HasMany(x => x.Models)
                .WithOne(x => x.Brand)
                .HasForeignKey(s => s.IdBrand)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            //Finition
            modelBuilder.Entity<Finition>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Finition>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Finition>()
                .Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
            modelBuilder.Entity<Finition>()
                .HasIndex(x => x.Name)
                .IsUnique();

            //Reparations
            modelBuilder.Entity<Repairing>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Repairing>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<Repairing>()
                .Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
            modelBuilder.Entity<Repairing>()
                .Property(x => x.Cost)
                .IsRequired();

            //Vehiule
            modelBuilder.Entity<Vehicle>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Vehicle>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Vehicle>()
                .Property(x => x.VinCode)
                .HasMaxLength(255)
                .IsRequired(false);
            modelBuilder.Entity<Vehicle>()
                .Property(x => x.ImageFileName)
                .HasMaxLength(255)
                .IsRequired(false);
            modelBuilder.Entity<Vehicle>()
                .Property(x => x.PurchasePrice)
                .HasDefaultValue(0d)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .Property(x => x.SellingPrice)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasOne(o => o.Brand)
                .WithMany(m => m.Vehicles)
                .HasForeignKey(f => f.BrandId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            
            //DeleteBehavior.NoAction car : ...peut provoquer des cycles ou des accès en cascade multiples entre Brand <-> Model <-> Vehicle
            modelBuilder.Entity<Vehicle>()
                .HasOne(o => o.Model)
                .WithMany(m => m.Vehicles)
                .HasForeignKey(f => f.ModelId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasOne(o => o.Finition)
                .WithMany(m => m.Vehicles)
                .HasForeignKey(f => f.FinitionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasOne(o => o.VehicleYear)
                .WithMany(m => m.Vehicles)
                .HasForeignKey(f => f.VehicleYearId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasMany(m => m.Repairings)
                .WithOne(o => o.Vehicle)
                .HasForeignKey(f => f.IdVehicle)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
