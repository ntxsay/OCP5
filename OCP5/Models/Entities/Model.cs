namespace OCP5.Models.Entities;

public class Model
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public virtual List<Vehicle> Vehicles { get; set; } = [];

}