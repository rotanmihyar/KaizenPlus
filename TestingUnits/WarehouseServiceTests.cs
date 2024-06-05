using Moq;
using kaizenplus.DataAccess.UnitOfWorks;
using kaizenplus.Domain.Warehouses;
using kaizenplus.Models;
using kaizenplus.Security;
using kaizenplus.Services.WarehouseServices;
using kaizenplus.Services.WarehouseServices.Models;
using Microsoft.EntityFrameworkCore.Query;
using NUnit.Framework;
using kaizenplus.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using kaizenplus;
using Microsoft.Extensions.DependencyInjection;
using kaizenplus.Localizations;
using System.Linq.Expressions;

[TestClass]
public class WarehouseServiceTests
{
    private Mock<IUnitOfWork>? _unitOfWorkMock;
    private Mock<IRepository<Warehouse>>? _warehouseRepositoryMock;
    private Mock<ISecurityManager>? _securityManagerMock;
    private Mock<IHttpContextAccessor>? _httpContextAccessorMock;
    private Mock<IJsonStringLocalizer>? _jsonStringLocalizerMock;
    private WarehouseService? _warehouseService;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _warehouseRepositoryMock = new Mock<IRepository<Warehouse>>();
        _securityManagerMock = new Mock<ISecurityManager>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _jsonStringLocalizerMock = new Mock<IJsonStringLocalizer>();

        // Setup the unitOfWork to return the mock repository
        _unitOfWorkMock.Setup(uow => uow.WarehouseRepository).Returns(_warehouseRepositoryMock.Object);

        // Create a service collection and add necessary services
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(_httpContextAccessorMock.Object);
        serviceCollection.AddSingleton(_jsonStringLocalizerMock.Object); // Add the mock IJsonStringLocalizer
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Create a default HttpContext and setup IHttpContextAccessor
        var context = new DefaultHttpContext
        {
            RequestServices = serviceProvider
        };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);

        // Optionally, you can set a user with claims
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        }, "mock"));
        context.User = user;

        // Configure the AppHttpContext to use the mock IHttpContextAccessor
        AppHttpContext.Configure(_httpContextAccessorMock.Object);

        // Setup SecurityManager to use the mock IHttpContextAccessor
        _ = _securityManagerMock.Setup(sm => sm.GetUserId())
                                .Returns(Guid.Parse(input: user.FindFirst(ClaimTypes.NameIdentifier).Value));

        // Setup the mock string localizer to return a specific string
        _jsonStringLocalizerMock.Setup(sl => sl.GetString(It.IsAny<string>(), It.IsAny<string>()));

        // Setup SaveAsync to return a completed task
        _unitOfWorkMock.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask);

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
        _securityManagerMock.Setup(sm => sm.GetUserId()).Returns(Guid.NewGuid());

        // Act
        var result = await _warehouseService.Create(input);

        // Assert
        _unitOfWorkMock.Verify(uow => uow.WarehouseRepository.Create(It.IsAny<Warehouse>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Once);
        NUnit.Framework.Assert.That(result, Is.Not.Null);
    }



    [Test]
    public void Get_ShouldReturnNotFound_WhenWarehouseDoesNotExist()
    {
        // Arrange
        _ = _unitOfWorkMock.Setup(uow => uow.WarehouseRepository.FirstOrDefault(
            x => x.Id == 1 && !x.IsDeleted,
            It.IsAny<Func<IQueryable<Warehouse>, IIncludableQueryable<Warehouse, object>>>()))
            .Returns((Warehouse)null);

        // Act
        var result = _warehouseService.Get(1);

        // Assert
        NUnit.Framework.Assert.That(result.Data, Is.Null);
        NUnit.Framework.Assert.That(result.ErrorCode, Is.EqualTo(ErrorCode.NotFound));
    }

    [Test]
    public async Task Delete_ShouldDeleteWarehouse()
    {
        // Arrange
        var warehouseId = 1;
        var warehouse = new Warehouse { Id = warehouseId, IsDeleted = false };

        _unitOfWorkMock.Setup(uow => uow.WarehouseRepository.FirstOrDefault(
            It.IsAny<Expression<Func<Warehouse, bool>>>(),
            It.IsAny<Func<IQueryable<Warehouse>, IIncludableQueryable<Warehouse, object>>>()))
            .Returns(warehouse);

        // Act
        var result = await _warehouseService.Delete(warehouseId);

        // Assert
        _unitOfWorkMock.Verify(uow => uow.WarehouseRepository.FirstOrDefault(
            It.IsAny<Expression<Func<Warehouse, bool>>>(),
            It.IsAny<Func<IQueryable<Warehouse>, IIncludableQueryable<Warehouse, object>>>()
        ), Times.Once);

        _unitOfWorkMock.Verify(uow => uow.WarehouseRepository.Update(warehouse), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Once);

        NUnit.Framework.Assert.That(result, Is.Not.Null);
        NUnit.Framework.Assert.That(result.ErrorCode, Is.EqualTo(ErrorCode.Success));
    }
}
