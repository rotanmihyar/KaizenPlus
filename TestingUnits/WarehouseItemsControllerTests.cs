using kaizenplus.Controllers;
using kaizenplus.Models;
using kaizenplus.Services.WarehouseItemServices;
using kaizenplus.Services.WarehouseItemServices.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TestingUnits
{
    public class WarehouseItemsControllerTests
    {

        [Fact]
        public async Task Create_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IWarehouseItemService>();
            mockService.Setup(service => service.Create(It.IsAny<WarehouseItemsInput>())).ReturnsAsync(new BaseResponse());

            var controller = new WarehouseItemsController(mockService.Object);
            var inputModel = new WarehouseItemsInput();

            // Act
            var result = await controller.Create(inputModel) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.Equals(200, result.StatusCode);

            var baseResponse = result.Value as BaseResponse;
            Assert.IsNotNull(baseResponse);
            Assert.IsNull(baseResponse.ErrorMessage);
        }

        [Fact]
        public async Task Get_ValidId_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IWarehouseItemService>();
            var outputModel = new WarehouseItemsOutput(null);
            mockService.Setup(service => service.Get(It.IsAny<long>())).Returns(new BaseResponse<WarehouseItemsOutput>(outputModel));

            var controller = new WarehouseItemsController(mockService.Object);
            var id = 1;

            // Act
            var result = controller.Get(id).Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.Equals(200, result.StatusCode);

            var baseResponse = result.Value as BaseResponse<WarehouseItemsOutput>;
            Assert.IsNotNull(baseResponse);
            Assert.Equals(outputModel, baseResponse.Data);
        }

        [Fact]
        public async Task Get_ValidSearch_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IWarehouseItemService>();
            var outputModel = new PageOutput<WarehouseItemsOutput>();
            mockService.Setup(service => service.Get(It.IsAny<WarehouseItemSearch>())).Returns(new BaseResponse<PageOutput<WarehouseItemsOutput>>(outputModel));

            var controller = new WarehouseItemsController(mockService.Object);
            var search = new WarehouseItemSearch();

            // Act
            var result = controller.Get(search).Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.Equals(200, result.StatusCode);

            var baseResponse = result.Value as BaseResponse<PageOutput<WarehouseItemsOutput>>;
            Assert.IsNotNull(baseResponse);
            Assert.Equals(outputModel, baseResponse.Data);
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<IWarehouseItemService>();
            mockService.Setup(service => service.Delete(It.IsAny<long>())).ReturnsAsync(new BaseResponse());

            var controller = new WarehouseItemsController(mockService.Object);
            var id = 1;

            // Act
            var result = await controller.Delete(id) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var baseResponse = result.Value as BaseResponse;
            Assert.NotNull(baseResponse);
            Assert.Null(baseResponse.ErrorMessage);
        }
    }
}
