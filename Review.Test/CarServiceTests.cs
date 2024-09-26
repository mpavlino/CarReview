using Moq;
using NUnit.Framework;
using Review.Services;
using Review.Model.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Review.Model;
using Review.DAL;
using Review.Handlers;

namespace Review.Test {
    public class CarServiceTests {
        private CarService _carService;
        private Mock<HttpClient> _httpClientMock;
        private Mock<ILogger<CarService>> _loggerMock;
        private Mock<IBrandService> _brandServiceMock;
        private Mock<TokenHandler> _tokenHandlerMock;
        private Mock<UserManager<AppUser>> _userManagerMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<CarManagerDbContext> _dbContextMock;

        [SetUp]
        public void Setup() {
            _httpClientMock = new Mock<HttpClient>();
            _loggerMock = new Mock<ILogger<CarService>>();
            _brandServiceMock = new Mock<IBrandService>();
            _tokenHandlerMock = new Mock<TokenHandler>();
            _userManagerMock = new Mock<UserManager<AppUser>>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _dbContextMock = new Mock<CarManagerDbContext>();

            _carService = new CarService(
                _dbContextMock.Object,
                _httpClientMock.Object,
                _tokenHandlerMock.Object,
                _loggerMock.Object,
                _userManagerMock.Object,
                _httpContextAccessorMock.Object,
                _brandServiceMock.Object
            );
        }

        [Test]
        public async Task SyncCarsAsync_ShouldCallGetAllCarsFromWebAsync() {
            // Arrange: Mock GetAllBrandsAsync and GetModelsByBrandId
            _brandServiceMock.Setup( service => service.GetAllBrandsAsync() )
                             .ReturnsAsync( new List<Brand> { new Brand { ID = 1, Name = "TestBrand" } } );

            _brandServiceMock.Setup( service => service.GetModelsByBrandId( 1 ) )
                             .ReturnsAsync( new List<Model.Model> { new Model.Model { Id = 1, Name = "TestModel" } } );

            // Act
            var result = await _carService.SyncCarsAsync(1);

            // Assert
            Assert.IsTrue( result, "SyncCarsAsync should return true" );

            // Verify that CreateCarAsync is called
            _dbContextMock.Verify( db => db.Cars.Add( It.IsAny<Car>() ), Times.AtLeastOnce() );
        }
    }
}
