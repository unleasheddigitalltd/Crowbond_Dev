using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Tasks;
using FluentAssertions;
using Xunit;

namespace Crowbond.Modules.WMS.Domain.Tests.Tasks;

public class TaskLineTests
{
    [Fact]
    public void Create_ShouldCreateTaskLineWithCorrectProperties()
    {
        // Arrange
        var fromLocationId = Guid.NewGuid();
        var toLocationId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        decimal totalQty = 10.5m;

        // Act
        var taskLine = TaskLine.Create(fromLocationId, toLocationId, productId, totalQty);

        // Assert
        taskLine.Should().NotBeNull();
        taskLine.Id.Should().NotBeEmpty();
        taskLine.FromLocationId.Should().Be(fromLocationId);
        taskLine.ToLocationId.Should().Be(toLocationId);
        taskLine.ProductId.Should().Be(productId);
        taskLine.TotalQty.Should().Be(totalQty);
        taskLine.CompletedQty.Should().Be(0);
        taskLine.IsCompleted.Should().BeFalse();
        taskLine.DispatchMappings.Should().BeEmpty();
    }

    [Fact]
    public void AddDispatchMapping_ShouldAddMappingSuccessfully()
    {
        // Arrange
        var taskLine = CreateTaskLine();
        var dispatchLineId = Guid.NewGuid();
        decimal allocatedQty = 5.0m;

        // Act
        var result = taskLine.AddDispatchMapping(dispatchLineId, allocatedQty);

        // Assert
        result.IsSuccess.Should().BeTrue();
        taskLine.DispatchMappings.Should().HaveCount(1);
        var mapping = taskLine.DispatchMappings.First();
        mapping.DispatchLineId.Should().Be(dispatchLineId);
        mapping.AllocatedQty.Should().Be(allocatedQty);
        mapping.CompletedQty.Should().Be(0);
        mapping.IsCompleted.Should().BeFalse();
    }

    [Fact]
    public void AddDispatchMapping_ShouldFailWhenDispatchLineAlreadyMapped()
    {
        // Arrange
        var taskLine = CreateTaskLine();
        var dispatchLineId = Guid.NewGuid();
        taskLine.AddDispatchMapping(dispatchLineId, 5.0m);

        // Act
        var result = taskLine.AddDispatchMapping(dispatchLineId, 3.0m);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TaskErrors.DispatchLineAlreadyMapped(dispatchLineId));
        taskLine.DispatchMappings.Should().HaveCount(1);
    }

    [Fact]
    public void CompleteQuantity_ShouldUpdateCompletedQtySuccessfully()
    {
        // Arrange
        var taskLine = CreateTaskLine();
        decimal qtyToComplete = 3.5m;

        // Act
        var result = taskLine.CompleteQuantity(qtyToComplete);

        // Assert
        result.IsSuccess.Should().BeTrue();
        taskLine.CompletedQty.Should().Be(qtyToComplete);
        taskLine.IsCompleted.Should().BeFalse();
    }

    [Fact]
    public void CompleteQuantity_ShouldMarkAsCompletedWhenAllQuantityIsCompleted()
    {
        // Arrange
        var taskLine = CreateTaskLine(totalQty: 10.0m);
        
        // Act
        var result = taskLine.CompleteQuantity(10.0m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        taskLine.CompletedQty.Should().Be(10.0m);
        taskLine.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public void CompleteQuantity_ShouldFailWhenExceedingTotalQty()
    {
        // Arrange
        var taskLine = CreateTaskLine(totalQty: 10.0m);
        
        // Act
        var result = taskLine.CompleteQuantity(11.0m);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TaskErrors.ProductCompleteQtyExceedsRequestQty(taskLine.ProductId));
        taskLine.CompletedQty.Should().Be(0);
    }

    [Fact]
    public void CompleteQuantity_ShouldAllowMultipleCompletions()
    {
        // Arrange
        var taskLine = CreateTaskLine(totalQty: 10.0m);
        
        // Act
        var result1 = taskLine.CompleteQuantity(4.0m);
        var result2 = taskLine.CompleteQuantity(6.0m);

        // Assert
        result1.IsSuccess.Should().BeTrue();
        result2.IsSuccess.Should().BeTrue();
        taskLine.CompletedQty.Should().Be(10.0m);
        taskLine.IsCompleted.Should().BeTrue();
    }

    private static TaskLine CreateTaskLine(decimal totalQty = 10.5m)
    {
        return TaskLine.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            totalQty);
    }
}
