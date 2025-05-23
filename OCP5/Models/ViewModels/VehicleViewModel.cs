using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OCP5.Models.ViewModels;

public class VehicleViewModel
{
    public int Id { get; set; }
    
    [DisplayName("Marque")]
    [Required(ErrorMessage = "Veuillez sélectionner une marque.")]
    [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner une marque.")]
    public int BrandId { get; set; }
    
    [DisplayName("Modèle")]
    [Required(ErrorMessage = "Veuillez sélectionner un modèle.")]
    [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner un modèle.")]
    public int ModelId { get; set; }
    
    [DisplayName("Finition")]
    [Required(ErrorMessage = "Veuillez sélectionner une finition.")]
    [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner une finition.")]
    public int FinitionId { get; set; }
    
    [DisplayName("Année")]
    [Required (ErrorMessage = "Veuillez sélectionner une année.")]
    public int VehicleYearId { get; set; }
    
    [DisplayName("Prix de vente")]
    [Required(ErrorMessage = "Veuillez saisir un prix de vente.")]
    [DataType(DataType.Currency)]
    [Range(1, double.MaxValue, ErrorMessage = "Veuillez saisir un prix de vente valide.")]
    public double SellingPrice { get; set; }
    
    [Display(Name = "Visuel")]
    //[FileExtensions(Extensions = "jpg, png, jpeg", ErrorMessage = "Veuillez sélectionner un fichier image valide (jpg, png, jpeg).")]
    public IFormFile? File { get; set; }
    
    [BindNever]
    public string? ImageFileName { get; set; }

    public SelectList? Brands { get; set; }
    public SelectList? Models { get; set; }
    public SelectList? Finitions { get; set; }
    
    public SelectList? VehicleYears { get; set; }
}