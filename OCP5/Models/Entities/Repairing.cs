namespace OCP5.Models.Entities;

public class Repairing
{
    public int Id { get; set; }
    public int IdVehicle { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Cost { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
}