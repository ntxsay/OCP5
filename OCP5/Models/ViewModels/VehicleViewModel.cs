using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OCP5.Models.ViewModels;

public class VehicleViewModel
{
    public int Id { get; set; }
    
    [DisplayName("Marque")]
    [Required(ErrorMessage = "Veuillez sélectionner un marque.")]
    public int BrandId { get; set; }
    
    [DisplayName("Modèle")]
    [Required(ErrorMessage = "Veuillez sélectionner un modèle.")]
    public int ModelId { get; set; }
    
    [DisplayName("Finition")]
    [Required(ErrorMessage = "Veuillez sélectionner une finition.")]
    public int FinitionId { get; set; }
    
    [DisplayName("Année")]
    [Required (ErrorMessage = "Veuillez sélectionner une année.")]
    public int VehicleYearId { get; set; }
    
    
    [DisplayName("Prix de vente")]
    [Required(ErrorMessage = "Veuillez saisir un prix de vente.")]
    [DataType(DataType.Currency)]
    [Range(0, double.MaxValue, ErrorMessage = "Veuillez saisir un prix de vente valide.")]
    public double SellingPrice { get; set; }
    
    [Display(Name = "Visuel")]
    public IFormFile? File { get; set; }

    public SelectList? Brands { get; set; } = null!;
    public SelectList? Models { get; set; } = null!;
    public SelectList? Finitions { get; set; } = null!;
    
    public SelectList? VehicleYears { get; set; } = null!;
}