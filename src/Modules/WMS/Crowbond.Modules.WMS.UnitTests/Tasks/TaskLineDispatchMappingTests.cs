using Crowbond.Modules.WMS.Domain.Tasks;
using FluentAssertions;

namespace Crowbond.Modules.WMS.Domain.Tests.Tasks;

public class TaskLineDispatchMappingTests
{
    [Fact]
    public void Create_ShouldCreateMappingWithCorrectProperties()
    {
        // Arrange
        var taskLineId = Guid.NewGuid();
        var dispatchLineId = Guid.NewGuid();
        decimal allocatedQty = 5.0m;

        // Act
        var mapping = TaskLineDispatchMapping.Create(taskLineId, dispatchLineId, allocatedQty);

        // Assert
        mapping.Should().NotBeNull();
        mapping.Id.Should().NotBeEmpty();
        mapping.TaskLineId.Should().Be(taskLineId);
        mapping.DispatchLineId.Should().Be(dispatchLineId);
        mapping.AllocatedQty.Should().Be(allocatedQty);
        mapping.CompletedQty.Should().Be(0);
        mapping.IsCompleted.Should().BeFalse();
    }

    [Fact]
    public void CompleteQuantity_ShouldUpdateCompletedQtySuccessfully()
    {
        // Arrange
        var mapping = CreateMapping();
        decimal qtyToComplete = 2.0m;

        // Act
        var result = mapping.CompleteQuantity(qtyToComplete);

        // Assert
        result.IsSuccess.Should().BeTrue();
        mapping.CompletedQty.Should().Be(qtyToComplete);
        mapping.IsCompleted.Should().BeFalse();
    }

    [Fact]
    public void CompleteQuantity_ShouldMarkAsCompletedWhenAllQuantityIsCompleted()
    {
        // Arrange
        var mapping = CreateMapping(allocatedQty: 5.0m);
        
        // Act
        var result = mapping.CompleteQuantity(5.0m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        mapping.CompletedQty.Should().Be(5.0m);
        mapping.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public void CompleteQuantity_ShouldFailWhenExceedingAllocatedQty()
    {
        // Arrange
        var mapping = CreateMapping(allocatedQty: 5.0m);
        
        // Act
        var result = mapping.CompleteQuantity(6.0m);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TaskErrors.MappingCompleteQtyExceedsAllocatedQty(mapping.Id));
        mapping.CompletedQty.Should().Be(0);
    }

    [Fact]
    public void CompleteQuantity_ShouldAllowMultipleCompletions()
    {
        // Arrange
        var mapping = CreateMapping(allocatedQty: 5.0m);
        
        // Act
        var result1 = mapping.CompleteQuantity(2.0m);
        var result2 = mapping.CompleteQuantity(3.0m);

        // Assert
        result1.IsSuccess.Should().BeTrue();
        result2.IsSuccess.Should().BeTrue();
        mapping.CompletedQty.Should().Be(5.0m);
        mapping.IsCompleted.Should().BeTrue();
    }

    private static TaskLineDispatchMapping CreateMapping(decimal allocatedQty = 5.0m)
    {
        return TaskLineDispatchMapping.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            allocatedQty);
    }
}
