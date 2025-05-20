using Microsoft.EntityFrameworkCore;
using OCP5.Models.Entities;

namespace OCP5.Data
{
    public class VehicleExpressDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Repairing> Repairings { get; set; }
        public DbSet<Finition> Finitions { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<VehicleYear> VehicleYears { get; set; }
        public DbSet<PriceMargin> PriceMargins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //marge de prix
            modelBuilder.Entity<PriceMargin>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<PriceMargin>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<PriceMargin>()
                .Property(x => x.Price).IsRequired();

            //Année véhicules
            modelBuilder.Entity<VehicleYear>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<VehicleYear>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<VehicleYear>()
                .Property(x => x.Year).IsRequired();

            //Brand
            modelBuilder.Entity<Brand>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Brand>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Brand>()
                .Property(x => x.Name).IsRequired();

            //Model
            modelBuilder.Entity<Model>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Model>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Model>()
                .Property(x => x.Name).IsRequired();

            //Finition
            modelBuilder.Entity<Finition>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Finition>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Finition>()
                .Property(x => x.Name).IsRequired();

            //Reparations
            modelBuilder.Entity<Repairing>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Repairing>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Repairing>()
                .Property(x => x.Name)
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
                .Property(x => x.PurchasePrice)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasOne(o => o.Brand)
                .WithOne(o => o.Vehicle)
                .HasForeignKey<Brand>(f => f.Id)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasOne(o => o.Model)
                .WithOne(o => o.Vehicle)
                .HasForeignKey<Model>(f => f.Id)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasOne(o => o.Finition)
                .WithOne(o => o.Vehicle)
                .HasForeignKey<Finition>(f => f.Id)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasOne(o => o.VehicleYear)
                .WithOne(o => o.Vehicle)
                .HasForeignKey<VehicleYear>(f => f.Id)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasMany(m => m.Repairings)
                .WithOne(o => o.Vehicle)
                .HasForeignKey(f => f.IdVehicle)
                .IsRequired();
        }
    }
}
