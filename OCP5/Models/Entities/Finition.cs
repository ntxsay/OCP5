namespace OCP5.Models.Entities;

public class Finition
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Vehicle Vehicle { get; set; } = null!;

}