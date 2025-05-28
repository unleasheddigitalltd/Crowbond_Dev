using Crowbond.Common.Application.Clock;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Application.Tasks;
using Crowbond.Modules.WMS.Application.Tasks.Picking.Strategies;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Stocks;
using FluentAssertions;
using Moq;

namespace Crowbond.Modules.WMS.Domain.Tests.Tasks;

public class ConsolidatedPickingTaskServiceTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IStockRepository> _stockRepositoryMock;
    private readonly Mock<IProductPickingClassifier> _productPickingClassifierMock;
    private readonly Mock<ConsolidatedPickingStrategy> _consolidatedStrategyMock;
    private readonly Mock<IndividualPickingStrategy> _individualStrategyMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ConsolidatedPickingTaskService _service;

    public ConsolidatedPickingTaskServiceTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _stockRepositoryMock = new Mock<IStockRepository>();
        _productPickingClassifierMock = new Mock<IProductPickingClassifier>();
        _consolidatedStrategyMock = new Mock<ConsolidatedPickingStrategy>();
        _individualStrategyMock = new Mock<IndividualPickingStrategy>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        
        _service = new ConsolidatedPickingTaskService(
            _taskRepositoryMock.Object,
            _productRepositoryMock.Object,
            _stockRepositoryMock.Object,
            _productPickingClassifierMock.Object,
            _consolidatedStrategyMock.Object,
            _individualStrategyMock.Object,
            _unitOfWorkMock.Object);
            
    }

    [Fact]  
    public async Task AddDispatchToPickingTasks_ShouldCreateNewTasksWhenNoneExist()
    {
        // Arrange
        var dispatch = CreateDispatchWithLinesForCustomerOne();
        var sequence = CreateSequence();
        
        // Setup repository to return null for existing task (none exists)
        _taskRepositoryMock
            .Setup(r => r.GetByRouteLocationAndTypeAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<TaskType>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((TaskHeader)null);
            
        // Setup sequence
        _taskRepositoryMock
            .Setup(r => r.GetSequenceAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(sequence);
            
        // Setup product repository to return products with default locations
        var defaultLocationId = Guid.NewGuid();
        _productRepositoryMock
            .Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid productId, CancellationToken _) => CreateProductWithLocation(productId, defaultLocationId));
            
        // Capture inserted task headers
        var insertedTaskHeaders = new List<TaskHeader>();
        _taskRepositoryMock
            .Setup(r => r.Insert(It.IsAny<TaskHeader>()))
            .Callback<TaskHeader>(th => insertedTaskHeaders.Add(th));
            
        // Setup task line operations
        _taskRepositoryMock
            .Setup(r => r.AddTaskLine(It.IsAny<TaskLine>()));
        _taskRepositoryMock
            .Setup(r => r.AddTaskLineDispatchMapping(It.IsAny<TaskLineDispatchMapping>()));
            
        // Act
        var result = await _service.AddDispatchToPickingTasks(dispatch);

        // Assert
        result.IsSuccess.Should().BeTrue();
        
        // Verify task headers were created (one for item picking, one for bulk picking)
        insertedTaskHeaders.Should().HaveCount(2);
        insertedTaskHeaders.Should().Contain(th => th.TaskType == TaskType.PickingItem);
        insertedTaskHeaders.Should().Contain(th => th.TaskType == TaskType.PickingBulk);
        
        // Verify each task header has the correct properties
        foreach (var taskHeader in insertedTaskHeaders)
        {
            taskHeader.RouteTripId.Should().Be(dispatch.RouteTripId);
            taskHeader.ScheduledDeliveryDate.Should().Be(dispatch.RouteTripDate);
            taskHeader.Status.Should().Be(TaskHeaderStatus.NotAssigned);
        }
        
        // Verify save changes was called
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddDispatchToPickingTasks_ShouldAddToExistingTaskWhenAvailable()
    {
        // Arrange
        var dispatch = CreateDispatchWithLinesForCustomerOne();
        var existingTask = CreateTaskHeader(TaskType.PickingItem);
        
        // Setup repository to return existing task
        _taskRepositoryMock
            .Setup(r => r.GetByRouteLocationAndTypeAsync(
                It.IsAny<Guid>(), 
                It.IsAny<Guid>(), 
                TaskType.PickingItem, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingTask);
            
        // For bulk picking, return null (no existing task)
        _taskRepositoryMock
            .Setup(r => r.GetByRouteLocationAndTypeAsync(
                It.IsAny<Guid>(), 
                It.IsAny<Guid>(), 
                TaskType.PickingBulk, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((TaskHeader)null);
            
        // Setup sequence for new bulk task
        _taskRepositoryMock
            .Setup(r => r.GetSequenceAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateSequence());
            
        // Setup product repository
        var defaultLocationId = Guid.NewGuid();
        _productRepositoryMock
            .Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid productId, CancellationToken _) => CreateProductWithLocation(productId, defaultLocationId));
            
        // Setup task line operations
        _taskRepositoryMock
            .Setup(r => r.AddTaskLine(It.IsAny<TaskLine>()));
        _taskRepositoryMock
            .Setup(r => r.AddTaskLineDispatchMapping(It.IsAny<TaskLineDispatchMapping>()));
            
        // Act
        var result = await _service.AddDispatchToPickingTasks(dispatch);

        // Assert
        result.IsSuccess.Should().BeTrue();
        
        // Verify task lines were added to existing task
        existingTask.Lines.Should().NotBeEmpty();
        
        // Verify save changes was called
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddDispatchToPickingTasks_ShouldHandleEmptyDispatchLines()
    {
        // Arrange
        var dispatch = CreateEmptyDispatch();
        
        // Setup minimal mocks for empty dispatch
        _taskRepositoryMock
            .Setup(r => r.AddTaskLine(It.IsAny<TaskLine>()));
        _taskRepositoryMock
            .Setup(r => r.AddTaskLineDispatchMapping(It.IsAny<TaskLineDispatchMapping>()));
        
        // Act
        var result = await _service.AddDispatchToPickingTasks(dispatch);

        // Assert
        result.IsSuccess.Should().BeTrue();
        
        // Verify no tasks were created
        _taskRepositoryMock.Verify(r => r.Insert(It.IsAny<TaskHeader>()), Times.Never);
        
        // Verify save changes was still called
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    private static DispatchHeader CreateDispatchWithLinesForCustomerOne()
    {
        var dispatch = DispatchHeader.Create(
            "DISP-001",
            Guid.NewGuid(),
            new DateOnly(2025, 5, 25),
            "Route 1");
            
        // Add some item lines
        dispatch.AddLine(
            Guid.NewGuid(),
            "ORD-001",
            "Customer 1",
            Guid.NewGuid(),
            Guid.NewGuid(),
            5.0m,
            false); // Not bulk
        
            
        // Add some bulk lines
        dispatch.AddLine(
            Guid.NewGuid(),
            "ORD-001",
            "Customer 1",
            Guid.NewGuid(),
            Guid.NewGuid(),
            10.0m,
            true); // Bulk
            
        return dispatch;
    }
    
    private static DispatchHeader CreateEmptyDispatch()
    {
        return DispatchHeader.Create(
            "DISP-002",
            Guid.NewGuid(),
            new DateOnly(2025, 5, 25),
            "Route 2");
    }
    
    private static TaskHeader CreateTaskHeader(TaskType taskType)
    {
        var result = TaskHeader.Create(
            $"TASK-{taskType}-001",
            null,
            null,
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateOnly(2025, 5, 25),
            taskType);
            
        return result.Value;
    }
    
    private static Sequence CreateSequence() => Sequence.Task;
    
    private static Product CreateProductWithLocation(Guid productId, Guid locationId)
    {
        var result = Product.Create(
            "TEST-SKU",
            "Test Product",
            null,
            "Test Filter",
            "Each",
            "Test Inventory",
            Guid.NewGuid(),
            locationId,
            Guid.NewGuid(),
            Guid.NewGuid(),
            TaxRateType.Vat,
            12345,
            1.0m,
            "Test handling",
            false,
            "Test notes",
            10.0m,
            10.0m,
            10.0m,
            10.0m,
            false);
            
        // Use reflection to set the Id and DefaultLocation since the constructor generates a new one
        typeof(Product)
            .GetProperty(nameof(Product.Id))
            ?.SetValue(result.Value, productId);
            
        // Make sure DefaultLocation is set properly
        typeof(Product)
            .GetProperty(nameof(Product.DefaultLocation))
            ?.SetValue(result.Value, locationId);
            
        return result.Value;
    }
}
