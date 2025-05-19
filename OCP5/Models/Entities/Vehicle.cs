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
    
    public Brand Brand { get; set; }
    public Model Model { get; set; }
    public Finition Finition { get; set; }
    public VehicleYear VehicleYear { get; set; }
    public List<Repairing> Repairings { get; set; } = [];
}