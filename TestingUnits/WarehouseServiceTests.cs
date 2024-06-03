using Moq;
using kaizenplus.DataAccess.UnitOfWorks;
using kaizenplus.Domain.Warehouses;
using kaizenplus.Models;
using kaizenplus.Security;
using kaizenplus.Services.WarehouseServices;
using kaizenplus.Services.WarehouseServices.Models;
using Microsoft.EntityFrameworkCore.Query;
using NUnit.Framework;

[TestFixture]
public class WarehouseServiceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<ISecurityManager> _securityManagerMock;
    private WarehouseService _warehouseService;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _securityManagerMock = new Mock<ISecurityManager>();
        _warehouseService = new WarehouseService(_unitOfWorkMock.Object, _securityManagerMock.Object);
    }

    [Test]
    public async Task Create_ShouldCreateWarehouse()
    {
        // Arrange
        var input = new WarehouseInput
        {
            Name = "Test Warehouse",
            Address = "123 Test St",
            City = "Test City",
            CountryId = 1,
        };
        _securityManagerMock.Setup(sm => sm.GetUserId()).Returns(Guid.Empty);

        // Act
        var result = await _warehouseService.Create(input);

        // Assert
        _unitOfWorkMock.Verify(uow => uow.WarehouseRepository.Create(It.IsAny<Warehouse>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Once);
        NUnit.Framework.Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void Get_ShouldReturnWarehouse()
    {
        // Arrange
        var warehouse = new Warehouse { Id = 1, IsDeleted = false };
        _unitOfWorkMock.Setup(uow => uow.WarehouseRepository.FirstOrDefault(
            x => x.Id == warehouse.Id && !x.IsDeleted,
            It.IsAny<Func<IQueryable<Warehouse>, IIncludableQueryable<Warehouse, object>>>()))
            .Returns(warehouse);

        // Act
        var result = _warehouseService.Get(1);

        // Assert
        NUnit.Framework.Assert.That(result, Is.Not.Null);
        NUnit.Framework.Assert.That(result.Data.Id, Is.EqualTo(warehouse.Id));
    }

    [Test]
    public void Get_ShouldReturnNotFound_WhenWarehouseDoesNotExist()
    {
        // Arrange
        _unitOfWorkMock.Setup(uow => uow.WarehouseRepository.FirstOrDefault(
            x => x.Id == 1 && !x.IsDeleted,
            It.IsAny<Func<IQueryable<Warehouse>, IIncludableQueryable<Warehouse, object>>>()))
            .Returns((Warehouse)null);

        // Act
        var result = _warehouseService.Get(1);

        // Assert
        NUnit.Framework.Assert.That(result.Data, Is.Null);
        NUnit.Framework.Assert.That(result.ErrorCode, Is.EqualTo(ErrorCode.NotFound));
    }
}
