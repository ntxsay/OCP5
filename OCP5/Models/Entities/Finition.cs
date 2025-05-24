namespace OCP5.Models.Entities;

public class Finition
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public virtual List<Vehicle> Vehicles { get; set; } = [];
}