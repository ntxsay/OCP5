namespace OCP5.Models.Entities;

public class Repairing
{
    public int Id { get; set; }
    public int IdVehicle { get; set; }
    public string Repair { get; set; } = string.Empty;
    public double RepairCost { get; set; }
}