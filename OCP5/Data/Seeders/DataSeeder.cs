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
            context.VehicleYears.AddRange(Enumerable.Range(1990, 45).Select(s => new VehicleYear
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
        //Ajout des véhicules
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
                    BrandId = brands.Single(w => w.Name == "Mazda").Id,
                    ModelId = models.Single(w => w.Name == "Miata").Id,
                    FinitionId = finitions.Single(w => w.Name == "LE").Id,
                    VehicleYearId = years.Single(w => w.Year == 2019).Id,
                    VinCode = null,
                    ImageFileName = "2019-Mazda-MX-5-Miata.jpg",
                    PurchasePrice = 1_800,
                    SellingPrice = 9_900,
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
                    BrandId = brands.Single(w => w.Name == "Jeep").Id,
                    ModelId = models.Single(w => w.Name == "Liberty").Id,
                    FinitionId = finitions.Single(w => w.Name == "Sport").Id,
                    VehicleYearId = years.Single(w => w.Year == 2007).Id,
                    VinCode = null,
                    ImageFileName = "jeep_liberty.jpg",
                    PurchasePrice = 4_500,
                    SellingPrice = 5_350,
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
                    BrandId = brands.Single(w => w.Name == "Renault").Id,
                    ModelId = models.Single(w => w.Name == "Scénic").Id,
                    FinitionId = finitions.Single(w => w.Name == "TCe").Id,
                    VehicleYearId = years.Single(w => w.Year == 2007).Id,
                    VinCode = null,
                    ImageFileName = "renault_scenic.jpg",
                    PurchasePrice = 1_800,
                    SellingPrice = 2_990,
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
                    BrandId = brands.Single(w => w.Name == "Ford").Id,
                    ModelId = models.Single(w => w.Name == "Explorer").Id,
                    FinitionId = finitions.Single(w => w.Name == "XLT").Id,
                    VehicleYearId = years.Single(w => w.Year == 2017).Id,
                    VinCode = null,
                    ImageFileName = "ford_explorer.jpg",
                    PurchasePrice = 24_350,
                    SellingPrice = 25_950,
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
                    BrandId = brands.Single(w => w.Name == "Honda").Id,
                    ModelId = models.Single(w => w.Name == "Civic").Id,
                    FinitionId = finitions.Single(w => w.Name == "LX").Id,
                    VehicleYearId = years.Single(w => w.Year == 2008).Id,
                    VinCode = null,
                    ImageFileName = "honda_civic.jpg",
                    PurchasePrice = 4_000,
                    SellingPrice = 4_975,
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
                    BrandId = brands.Single(w => w.Name == "Volkswagen").Id,
                    ModelId = models.Single(w => w.Name == "GTI").Id,
                    FinitionId = finitions.Single(w => w.Name == "S").Id,
                    VehicleYearId = years.Single(w => w.Year == 2016).Id,
                    VinCode = null,
                    ImageFileName = "2016_volkswagen_gti-s.webp",
                    PurchasePrice = 15_250,
                    SellingPrice = 16_190,
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
                    BrandId = brands.Single(w => w.Name == "Ford").Id,
                    ModelId = models.Single(w => w.Name == "Edge").Id,
                    FinitionId = finitions.Single(w => w.Name == "SEL").Id,
                    VehicleYearId = years.Single(w => w.Year == 2013).Id,
                    VinCode = null,
                    ImageFileName = "ford_edge.jpg",
                    PurchasePrice = 10_990,
                    SellingPrice = 12_440,
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