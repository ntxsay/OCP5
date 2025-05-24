using OCP5.Models.Entities;
using OCP5.Models.ViewModels;

namespace OCP5.Extensions;

public static class ViewModelModelConverterExtension
{
    public static Vehicle ConvertToModel(this VehicleViewModel self)
    {
        return new Vehicle()
        {
            Id = self.Id,
            BrandId = self.BrandId,
            ModelId = self.ModelId,
            FinitionId = self.FinitionId,
            VehicleYearId = self.VehicleYearId,
            VinCode = null,
            PurchasePrice = 0,
            SellingPrice = self.SellingPrice,
            ImageFileName = self.ImageFileName,
        };
    }

    public static  VehicleViewModel ConvertToViewModel(this Vehicle self)
    {
        return new VehicleViewModel()
        {
            Id = self.Id,
            BrandId = self.BrandId,
            ModelId = self.ModelId,
            FinitionId = self.FinitionId,
            VehicleYearId = self.VehicleYearId,
            SellingPrice = self.SellingPrice,
            ImageFileName = self.ImageFileName,
        };
    }
    
    public static VehicleThumbnailViewModel ConvertToThumbnailViewModel(this Vehicle self)
    {
        return new VehicleThumbnailViewModel()
        {
            Id = self.Id,
            BrandName = self.Brand.Name,
            ModelName = self.Model.Name,
            FinitionName = self.Finition.Name,
            Year = self.VehicleYear.Year,
            SellingPrice = self.SellingPrice,
            ImageFileName = self.ImageFileName,
        };
    }
}