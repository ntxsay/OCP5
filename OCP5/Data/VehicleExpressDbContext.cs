using Microsoft.EntityFrameworkCore;
using System.Data;
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
    }
}
