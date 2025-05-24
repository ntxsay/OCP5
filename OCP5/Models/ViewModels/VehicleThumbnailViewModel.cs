namespace OCP5.Models.ViewModels;

public class VehicleThumbnailViewModel
{
    public int Id { get; set; }
    public int Year { get; set; }
    public string BrandName { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public string FinitionName { get; set; } = string.Empty;
    public double SellingPrice { get; set; }
    public string? ImageFileName { get; set; }
}