namespace OCP5.Models.Entities;

public class Model
{
    public int Id { get; set; }
    public int IdBrand { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual Brand Brand { get; set; } = null!;
    public virtual List<Vehicle> Vehicles { get; set; } = [];

}