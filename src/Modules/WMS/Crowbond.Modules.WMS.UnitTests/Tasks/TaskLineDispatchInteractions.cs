using Crowbond.Modules.WMS.Domain.Tasks;
using FluentAssertions;
using Xunit;

namespace Crowbond.Modules.WMS.UnitTests.Tasks;

public class TaskLineDispatchInteractions
{
    [Fact]
    public void TaskLine_WithDispatchMappings_ShouldTrackRelationships()
    {
        // Arrange
        var taskLine = TaskLine.Create(
            Guid.NewGuid(), // fromLocationId
            Guid.NewGuid(), // toLocationId
            Guid.NewGuid(), // productId
            10.0m);        // totalQty
            
        var dispatchLine1Id = Guid.NewGuid();
        var dispatchLine2Id = Guid.NewGuid();
        
        // Act
        var result1 = taskLine.AddDispatchMapping(dispatchLine1Id, 4.0m);
        var result2 = taskLine.AddDispatchMapping(dispatchLine2Id, 6.0m);
        
        // Assert
        result1.IsSuccess.Should().BeTrue();
        result2.IsSuccess.Should().BeTrue();
        
        taskLine.DispatchMappings.Should().HaveCount(2);
        
        var mapping1 = taskLine.DispatchMappings.First(m => m.DispatchLineId == dispatchLine1Id);
        var mapping2 = taskLine.DispatchMappings.First(m => m.DispatchLineId == dispatchLine2Id);
        
        mapping1.AllocatedQty.Should().Be(4.0m);
        mapping2.AllocatedQty.Should().Be(6.0m);
        
        // The sum of allocated quantities should equal the task line's total quantity
        taskLine.DispatchMappings.Sum(m => m.AllocatedQty).Should().Be(taskLine.TotalQty);
    }
    
    [Fact]
    public void TaskLine_WithDispatchMappings_ShouldPreventDuplicateMappings()
    {
        // Arrange
        var taskLine = TaskLine.Create(
            Guid.NewGuid(), // fromLocationId
            Guid.NewGuid(), // toLocationId
            Guid.NewGuid(), // productId
            10.0m);        // totalQty
            
        var dispatchLineId = Guid.NewGuid();
        
        // Act
        var result1 = taskLine.AddDispatchMapping(dispatchLineId, 5.0m);
        var result2 = taskLine.AddDispatchMapping(dispatchLineId, 5.0m); // Duplicate mapping
        
        // Assert
        result1.IsSuccess.Should().BeTrue();
        result2.IsFailure.Should().BeTrue();
        result2.Error.Should().Be(TaskErrors.DispatchLineAlreadyMapped(dispatchLineId));
        
        taskLine.DispatchMappings.Should().HaveCount(1);
    }
    
    [Fact]
    public void TaskLine_WithDispatchMappings_ShouldDistributeCompletedQuantityProportionally()
    {
        // Arrange
        var taskLine = TaskLine.Create(
            Guid.NewGuid(), // fromLocationId
            Guid.NewGuid(), // toLocationId
            Guid.NewGuid(), // productId
            10.0m);        // totalQty
            
        var dispatchLine1Id = Guid.NewGuid();
        var dispatchLine2Id = Guid.NewGuid();
        
        taskLine.AddDispatchMapping(dispatchLine1Id, 4.0m); // 40% of total
        taskLine.AddDispatchMapping(dispatchLine2Id, 6.0m); // 60% of total
        
        // Act - Complete 5 units (half of the total)
        var result = taskLine.CompleteQuantity(5.0m);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        
        var mapping1 = taskLine.DispatchMappings.First(m => m.DispatchLineId == dispatchLine1Id);
        var mapping2 = taskLine.DispatchMappings.First(m => m.DispatchLineId == dispatchLine2Id);
        
        // Mapping 1 should get 40% of 5.0 = 2.0 units
        mapping1.CompletedQty.Should().Be(2.0m);
        
        // Mapping 2 should get 60% of 5.0 = 3.0 units
        mapping2.CompletedQty.Should().Be(3.0m);
        
        // Total completed quantity should be 5.0
        taskLine.CompletedQty.Should().Be(5.0m);
        
        // Complete the remaining 5.0 units
        var result2 = taskLine.CompleteQuantity(5.0m);
        
        result2.IsSuccess.Should().BeTrue();
        
        // Mapping 1 should now have 40% of 10.0 = 4.0 units
        mapping1.CompletedQty.Should().Be(4.0m);
        mapping1.IsCompleted.Should().BeTrue();
        
        // Mapping 2 should now have 60% of 10.0 = 6.0 units
        mapping2.CompletedQty.Should().Be(6.0m);
        mapping2.IsCompleted.Should().BeTrue();
        
        // Task line should be fully completed
        taskLine.CompletedQty.Should().Be(10.0m);
        taskLine.IsCompleted.Should().BeTrue();
    }
    
    [Fact]
    public void TaskLine_WithDispatchMappings_ShouldHandleRoundingErrors()
    {
        // Arrange
        var taskLine = TaskLine.Create(
            Guid.NewGuid(), // fromLocationId
            Guid.NewGuid(), // toLocationId
            Guid.NewGuid(), // productId
            10.0m);        // totalQty
            
        // Create three mappings with non-round percentages
        var dispatchLine1Id = Guid.NewGuid();
        var dispatchLine2Id = Guid.NewGuid();
        var dispatchLine3Id = Guid.NewGuid();
        
        taskLine.AddDispatchMapping(dispatchLine1Id, 3.33m); // ~33.3%
        taskLine.AddDispatchMapping(dispatchLine2Id, 3.33m); // ~33.3%
        taskLine.AddDispatchMapping(dispatchLine3Id, 3.34m); // ~33.4%
        
        // Act - Complete all 10 units
        var result = taskLine.CompleteQuantity(10.0m);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        
        var mapping1 = taskLine.DispatchMappings.First(m => m.DispatchLineId == dispatchLine1Id);
        var mapping2 = taskLine.DispatchMappings.First(m => m.DispatchLineId == dispatchLine2Id);
        var mapping3 = taskLine.DispatchMappings.First(m => m.DispatchLineId == dispatchLine3Id);
        
        // Check that all mappings are completed
        mapping1.IsCompleted.Should().BeTrue();
        mapping2.IsCompleted.Should().BeTrue();
        mapping3.IsCompleted.Should().BeTrue();
        
        // Check that the sum of completed quantities equals the total
        var totalCompleted = mapping1.CompletedQty + mapping2.CompletedQty + mapping3.CompletedQty;
        totalCompleted.Should().Be(10.0m);
        
        // Task line should be fully completed
        taskLine.CompletedQty.Should().Be(10.0m);
        taskLine.IsCompleted.Should().BeTrue();
    }
}
