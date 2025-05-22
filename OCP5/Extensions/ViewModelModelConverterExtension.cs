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
        };
    }
}