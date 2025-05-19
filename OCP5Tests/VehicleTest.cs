using OCP5.Models.Entities;

namespace OCP5Tests
{
    public class VehicleTest
    {
        #region Brand

        [Theory]
        [InlineData("Mazda")]
        public void CheckBrandNameIsNotNullOrWhitespace(string brandName)
        {
            //Arrange
            var model = new Brand()
            {
                Id = 1,
                Name = brandName,
            };
            
            //Act
            var isEmptyNullOrWhiteSpace = string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name);
            
            //Assert
            Assert.IsType<Brand>(model);
            Assert.False(isEmptyNullOrWhiteSpace);
        }
        
        [Theory]
        [InlineData("Mazda", "Mazda")]
        public void CheckBrandNameEquals(string expectedBrandName, string brandName)
        {
            //Arrange
            var model = new Brand()
            {
                Id = 1,
                Name = brandName,
            };
            
            
            //Assert
            Assert.IsType<Brand>(model);
            Assert.Equal(expectedBrandName, model.Name);
        }

        #endregion
        
        #region Model

        [Theory]
        [InlineData("Miata")]
        public void CheckModelNameIsNotNullOrWhitespace(string modelName)
        {
            //Arrange
            var model = new Model()
            {
                Id = 1,
                Name = modelName,
            };
            
            //Act
            var isEmptyNullOrWhiteSpace = string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name);
            
            //Assert
            Assert.IsType<Model>(model);
            Assert.False(isEmptyNullOrWhiteSpace);
        }
        
        [Theory]
        [InlineData("Miata", "Miata")]
        public void CheckModelNameEquals(string expectedModelName, string modelName)
        {
            //Arrange
            var model = new Model()
            {
                Id = 1,
                Name = modelName,
            };
            
            
            //Assert
            Assert.IsType<Model>(model);
            Assert.Equal(expectedModelName, model.Name);
        }

        #endregion
        
        #region Finition

        [Theory]
        [InlineData("LE")]
        public void CheckFinitionNameIsNotNullOrWhitespace(string finitionName)
        {
            //Arrange
            var model = new Finition()
            {
                Id = 1,
                Name = finitionName,
            };
            
            //Act
            var isEmptyNullOrWhiteSpace = string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name);
            
            //Assert
            Assert.IsType<Finition>(model);
            Assert.False(isEmptyNullOrWhiteSpace);
        }
        
        [Theory]
        [InlineData("LE", "LE")]
        public void CheckFinitionNameEquals(string expectedFinitionName, string finitionName)
        {
            //Arrange
            var model = new Finition()
            {
                Id = 1,
                Name = finitionName,
            };
            
            
            //Assert
            Assert.IsType<Finition>(model);
            Assert.Equal(expectedFinitionName, model.Name);
        }

        #endregion
        
        #region Reparairing

        [Theory]
        [InlineData(1, "Restauration complète")]
        public void CheckRepairNameIsNotNullOrWhitespace(int idVehicle, string repairName)
        {
            //Arrange
            var model = new Repairing()
            {
                Id = 1,
                IdVehicle = idVehicle,
                Name = repairName,
                Cost = 7600
            };
            
            //Act
            var isEmptyNullOrWhiteSpace = string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name);
            
            //Assert
            Assert.IsType<Repairing>(model);
            Assert.False(isEmptyNullOrWhiteSpace);
        }
        
        [Theory]
        [InlineData(1,"Restauration complète", "Restauration complète")]
        public void CheckRepairNameEquals(int idVehicle, string expectedRepairName, string repairName)
        {
            //Arrange
            var model = new Repairing()
            {
                Id = 1,
                IdVehicle = idVehicle,
                Name = repairName,
                Cost = 7600
            };
            
            
            //Assert
            Assert.IsType<Repairing>(model);
            Assert.Equal(expectedRepairName, model.Name);
        }
        
        [Theory]
        [InlineData(1, 7600)]
        public void CheckRepairCostValid(int idVehicle, double cost)
        {
            //Arrange
            var model = new Repairing()
            {
                Id = 1,
                IdVehicle = idVehicle,
                Name = "Restauration complète",
                Cost = cost
            };
            
            //Act
            var isCostValid = model.Cost > 0d;
            
            //Assert
            Assert.IsType<Repairing>(model);
            Assert.True(isCostValid);
        }
        
        
        [Theory]
        [InlineData(1, 7600,7600)]
        public void CheckRepairCostEqual(int idVehicle, double expectedCost, double cost)
        {
            //Arrange
            var model = new Repairing()
            {
                Id = 1,
                IdVehicle = idVehicle,
                Name = "Restauration complète",
                Cost = cost
            };
            
            
            //Assert
            Assert.IsType<Repairing>(model);
            Assert.Equal(expectedCost, model.Cost, model.Cost);
        }

        #endregion
        
        #region VehicleYear

        [Theory]
        [InlineData(2017, 1990, 2035)]
        public void CheckVehicleYearValid(int vehicleYear, int minYear, int maxYear)
        {
            //Arrange
            var model = new VehicleYear()
            {
                Id = 1,
                Year = vehicleYear,
            };
            
            //Act
            var isYearValid = model.Year >= minYear && model.Year <= maxYear;
            
            //Assert
            Assert.IsType<VehicleYear>(model);
            Assert.True(isYearValid);
        }
        
        
        [Theory]
        [InlineData(2017, 2017)]
        public void CheckVehicleYearEqual(int expectedVehicleYear, int vehicleYear)
        {
            //Arrange
            var model = new VehicleYear()
            {
                Id = 1,
                Year = vehicleYear,
            };
            
            
            //Assert
            Assert.IsType<VehicleYear>(model);
            Assert.Equal(expectedVehicleYear, model.Year);
        }

        #endregion

        
        [Fact]
        public void Test1()
        {
            var brand = new Brand()
            {
                Id = 1,
                Name = "Mazda",
            };

            var model = new Model()
            {
                Id = 1,
                Name = "Miata",
            };

            var finition = new Finition()
            {
                Id = 1,
                Name = "LE",
            };

            var vehicleYear = new VehicleYear()
            {
                Id = 1,
                Year = 2017,
            };

            var vehicle = new Vehicle()
            {
                Id = 1,
                BrandId = brand.Id,
                Brand = brand,
                ModelId = model.Id,
                Model = model,
                FinitionId = finition.Id,
                Finition = finition,
                VehicleYearId = vehicleYear.Id,
                VehicleYear = vehicleYear,
                VinCode = null,
                PurchasePrice = 1800,
            };
        }
    }
}
