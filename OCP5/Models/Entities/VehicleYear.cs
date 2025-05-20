namespace OCP5.Models.Entities;

public class VehicleYear
{
    public int Id { get; set; }
    public int Year { get; set; }
    public Vehicle Vehicle { get; set; } = null!;

}