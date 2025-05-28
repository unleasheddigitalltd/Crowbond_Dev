using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Tasks;
using FluentAssertions;
using Xunit;

namespace Crowbond.Modules.WMS.UnitTests.Tasks;

public class TaskLineDispatchMappingInteractionTests
{
    [Fact]
    public void TaskLine_ShouldTrackCompletionStatus()
    {
        // Arrange
        var taskLine = TaskLine.Create(
            Guid.NewGuid(), // fromLocationId
            Guid.NewGuid(), // toLocationId
            Guid.NewGuid(), // productId
            10.0m);        // totalQty
            
        // Act
        var result = taskLine.CompleteQuantity(5.0m);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        taskLine.CompletedQty.Should().Be(5.0m);
        taskLine.IsCompleted.Should().BeFalse(); // Only 50% complete
        
        // Complete the remaining quantity
        var result2 = taskLine.CompleteQuantity(5.0m);
        
        result2.IsSuccess.Should().BeTrue();
        taskLine.CompletedQty.Should().Be(10.0m);
        taskLine.IsCompleted.Should().BeTrue(); // Now 100% complete
    }
    
    [Fact]
    public void TaskLineDispatchMapping_ShouldTrackCompletionStatus()
    {
        // Arrange
        var mapping = TaskLineDispatchMapping.Create(
            Guid.NewGuid(), // taskLineId
            Guid.NewGuid(), // dispatchLineId
            10.0m);        // allocatedQty
            
        // Act
        var result = mapping.CompleteQuantity(5.0m);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        mapping.CompletedQty.Should().Be(5.0m);
        mapping.IsCompleted.Should().BeFalse(); // Only 50% complete
        
        // Complete the remaining quantity
        var result2 = mapping.CompleteQuantity(5.0m);
        
        result2.IsSuccess.Should().BeTrue();
        mapping.CompletedQty.Should().Be(10.0m);
        mapping.IsCompleted.Should().BeTrue(); // Now 100% complete
    }
    
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
    public void DispatchLine_ShouldBeUpdatedWhenPickingTaskLineIsCompleted()
    {
        // Arrange - Create a dispatch line
        var dispatchHeader = DispatchHeader.Create(
            "DISP-001",
            Guid.NewGuid(),
            new DateOnly(2025, 5, 25),
            "Route 1");
            
        var dispatchLine = dispatchHeader.AddLine(
            Guid.NewGuid(), // orderId
            "ORD-001",      // orderNo
            "Customer 1",   // customerBusinessName
            Guid.NewGuid(), // orderLineId
            Guid.NewGuid(), // productId
            10.0m,          // orderedQty
            false);         // isBulk
            
        // Start processing the dispatch
        dispatchHeader.StartProcessing();
        
        // Act - Pick the dispatch line
        var pickResult = dispatchHeader.PickLine(dispatchLine.Id, 10.0m);
        
        // Assert
        pickResult.IsSuccess.Should().BeTrue();
        dispatchLine.PickedQty.Should().Be(10.0m);
        dispatchLine.IsPicked.Should().BeTrue();
        
        // Finalize the picking
        var finalizeResult = dispatchHeader.FinalizeLinePicking(dispatchLine.Id);
        
        finalizeResult.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public void DispatchLine_ShouldBePartiallyUpdatedWhenPickingTaskLineIsPartiallyCompleted()
    {
        // Arrange - Create a dispatch line
        var dispatchHeader = DispatchHeader.Create(
            "DISP-001",
            Guid.NewGuid(),
            new DateOnly(2025, 5, 25),
            "Route 1");
            
        var dispatchLine = dispatchHeader.AddLine(
            Guid.NewGuid(), // orderId
            "ORD-001",      // orderNo
            "Customer 1",   // customerBusinessName
            Guid.NewGuid(), // orderLineId
            Guid.NewGuid(), // productId
            10.0m,          // orderedQty
            false);         // isBulk
            
        // Start processing the dispatch
        dispatchHeader.StartProcessing();
        
        // Act - Pick the dispatch line partially
        var pickResult = dispatchHeader.PickLine(dispatchLine.Id, 5.0m);
        
        // Assert
        pickResult.IsSuccess.Should().BeTrue();
        dispatchLine.PickedQty.Should().Be(5.0m);
        dispatchLine.IsPicked.Should().BeFalse(); // Not fully picked
        
        // Pick the remaining quantity
        var pickResult2 = dispatchHeader.PickLine(dispatchLine.Id, 5.0m);
        
        pickResult2.IsSuccess.Should().BeTrue();
        dispatchLine.PickedQty.Should().Be(10.0m);
        dispatchLine.IsPicked.Should().BeTrue(); // Now fully picked
    }
}
