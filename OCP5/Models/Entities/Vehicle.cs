namespace OCP5.Models.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public int ModelId { get; set; }
    public int FinitionId { get; set; }
    public int VehicleYearId { get; set; }
    public string? VinCode { get; set; }
    public double PurchasePrice { get; set; }
    public double SellingPrice { get; set; }
    
    public Brand Brand { get; set; } = null!;
    public Model Model { get; set; } = null!;
    public Finition Finition { get; set; } = null!;
    public VehicleYear VehicleYear { get; set; } = null!;
    public List<Repairing> Repairings { get; set; } = [];
}