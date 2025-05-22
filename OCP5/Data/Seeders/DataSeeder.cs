using Microsoft.EntityFrameworkCore;
using OCP5.Models.Entities;

namespace OCP5.Data.Seeders;

internal class DataSeeder
{
    internal static void Initialize(IServiceProvider serviceProvider, IConfiguration config)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>(), config);

        InsertSelectors(context);
        InsertVehicules(context);
    }

    /// <summary>
    /// Insert les enregistrements dans les tables dépendantes de la table Vehicle
    /// </summary>
    /// <param name="context"></param>
    private static void InsertSelectors(ApplicationDbContext context)
    {
        //Ajout des années des véhicules
        if (!context.VehicleYears.Any())
        {
            HashSet<int> values = [2019, 2007, 2017, 2008, 2016, 2013];
            context.VehicleYears.AddRange(values.Select(s => new VehicleYear
            {
                Year = s
            }));
        
            context.SaveChanges();
        }

        //Ajout des marques de véhicules
        if (!context.Brands.Any())
        {
            var values = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Mazda",
                "Jeep",
                "Renault",
                "Ford",
                "Honda",
                "Volkswagen",
            };
            context.Brands.AddRange(values.Select(s => new Brand
            {
                Name = s
            }));
            context.SaveChanges();
        }
        
        //Ajout des modeles de véhicules
        if (!context.Models.Any())
        {
            var values = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Miata",
                "Liberty",
                "Scénic",
                "Explorer",
                "Civic",
                "GTI",
                "Edge",
            };
            context.Models.AddRange(values.Select(s => new Model
            {
                Name = s
            }));
            context.SaveChanges();
        }
        
        //Ajout des finitions de véhicules
        if (!context.Finitions.Any())
        {
            var values = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "LE",
                "Sport",
                "TCe",
                "XLT",
                "LX",
                "S",
                "SEL",
            };
            context.Finitions.AddRange(values.Select(s => new Finition
            {
                Name = s
            }));
            context.SaveChanges();
        }
    }
    
    /// <summary>
    /// Insert les enregistrements dans la table Vehicle
    /// </summary>
    /// <param name="context"></param>
    private static void InsertVehicules(ApplicationDbContext context)
    {
        //Ajout des années des véhicules
        if (!context.Vehicles.Any())
        {
            var years = context.VehicleYears.Where(w =>
                w.Year == 2019 || w.Year == 2007 || w.Year == 2017 || w.Year == 2008 || w.Year == 2016 ||
                w.Year == 2013).ToArray();
            
            var brands = context.Brands.Where(w =>
                w.Name == "Mazda" || w.Name == "Jeep" || w.Name == "Renault" || w.Name == "Ford" ||
                w.Name == "Honda" || w.Name == "Volkswagen").ToArray();
            
            var models = context.Models.Where(w =>
                w.Name == "Miata" || w.Name == "Liberty" || w.Name == "Scénic" || w.Name == "Explorer" ||
                w.Name == "Civic" || w.Name == "GTI" || w.Name == "Edge").ToArray();
            
            var finitions = context.Finitions.Where(w =>
                w.Name == "LE" || w.Name == "Sport" || w.Name == "TCe" || w.Name == "XLT" ||
                w.Name == "LX" || w.Name == "S" || w.Name == "SEL").ToArray();
            
            var vehicles = new List<Vehicle>
            {
                new()
                {
                    Brand = brands.Single(w => w.Name == "Mazda"),
                    Model = models.Single(w => w.Name == "Miata"),
                    Finition = finitions.Single(w => w.Name == "LE"),
                    VehicleYear = years.Single(w => w.Year == 2019),
                    VinCode = null,
                    PurchasePrice = 1_800,
                    Repairings = 
                    [
                        new Repairing
                        {
                            Name = "Restauration complète",
                            Cost = 7_600
                        }
                    ]
                },
                new()
                {
                    Brand = brands.Single(w => w.Name == "Jeep"),
                    Model = models.Single(w => w.Name == "Liberty"),
                    Finition = finitions.Single(w => w.Name == "Sport"),
                    VehicleYear = years.Single(w => w.Year == 2007),
                    VinCode = null,
                    PurchasePrice = 4_500,
                    Repairings = 
                    [
                        new Repairing
                        {
                            Name = "Roulements des roues avant",
                            Cost = 350
                        }
                    ]
                },
                new()
                {
                    Brand = brands.Single(w => w.Name == "Renault"),
                    Model = models.Single(w => w.Name == "Scénic"),
                    Finition = finitions.Single(w => w.Name == "TCe"),
                    VehicleYear = years.Single(w => w.Year == 2007),
                    VinCode = null,
                    PurchasePrice = 1_800,
                    Repairings = 
                    [
                        new Repairing
                        {
                            Name = "Radiateur, freins",
                            Cost = 690
                        }
                    ]
                },
                new()
                {
                    Brand = brands.Single(w => w.Name == "Ford"),
                    Model = models.Single(w => w.Name == "Explorer"),
                    Finition = finitions.Single(w => w.Name == "XLT"),
                    VehicleYear = years.Single(w => w.Year == 2017),
                    VinCode = null,
                    PurchasePrice = 24_350,
                    Repairings = 
                    [
                        new Repairing
                        {
                            Name = "Pneus, freins",
                            Cost = 1_100
                        }
                    ]
                },
                new()
                {
                    Brand = brands.Single(w => w.Name == "Honda"),
                    Model = models.Single(w => w.Name == "Civic"),
                    Finition = finitions.Single(w => w.Name == "LX"),
                    VehicleYear = years.Single(w => w.Year == 2008),
                    VinCode = null,
                    PurchasePrice = 4_000,
                    Repairings = 
                    [
                        new Repairing
                        {
                            Name = "Climatisation, freins",
                            Cost = 475
                        }
                    ]
                },
                new()
                {
                    Brand = brands.Single(w => w.Name == "Volkswagen"),
                    Model = models.Single(w => w.Name == "GTI"),
                    Finition = finitions.Single(w => w.Name == "S"),
                    VehicleYear = years.Single(w => w.Year == 2016),
                    VinCode = null,
                    PurchasePrice = 15_250,
                    Repairings = 
                    [
                        new Repairing
                        {
                            Name = "Pneus",
                            Cost = 440
                        }
                    ]
                },
                new()
                {
                    Brand = brands.Single(w => w.Name == "Ford"),
                    Model = models.Single(w => w.Name == "Edge"),
                    Finition = finitions.Single(w => w.Name == "SEL"),
                    VehicleYear = years.Single(w => w.Year == 2013),
                    VinCode = null,
                    PurchasePrice = 10_990,
                    Repairings = 
                    [
                        new Repairing
                        {
                            Name = "Pneus, freins, climatisation",
                            Cost = 950
                        }
                    ]
                }
            };
        
            context.Vehicles.AddRange(vehicles);
            context.SaveChanges();
        }
    }
}