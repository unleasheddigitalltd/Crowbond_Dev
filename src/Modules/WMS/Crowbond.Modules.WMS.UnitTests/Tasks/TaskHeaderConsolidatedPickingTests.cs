using Crowbond.Modules.WMS.Domain.Tasks;
using FluentAssertions;

namespace Crowbond.Modules.WMS.Domain.Tests.Tasks;

public class TaskHeaderConsolidatedPickingTests
{
    [Fact]
    public void Create_WithLocationAndRouteTripInfo_ShouldCreateTaskHeaderWithCorrectProperties()
    {
        // Arrange
        var taskNo = "PICK-001";
        var locationId = Guid.NewGuid();
        var routeTripId = Guid.NewGuid();
        var scheduledDeliveryDate = new DateOnly(2025, 5, 25);
        var taskType = TaskType.PickingItem;

        // Act
        var result = TaskHeader.Create(
            taskNo,
            null,
            null,
            locationId,
            routeTripId,
            scheduledDeliveryDate,
            taskType);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var taskHeader = result.Value;
        taskHeader.Should().NotBeNull();
        taskHeader.TaskNo.Should().Be(taskNo);
        taskHeader.ReceiptId.Should().BeNull();
        taskHeader.DispatchId.Should().BeNull();
        taskHeader.LocationId.Should().Be(locationId);
        taskHeader.RouteTripId.Should().Be(routeTripId);
        taskHeader.ScheduledDeliveryDate.Should().Be(scheduledDeliveryDate);
        taskHeader.TaskType.Should().Be(taskType);
        taskHeader.Status.Should().Be(TaskHeaderStatus.NotAssigned);
        taskHeader.Lines.Should().BeEmpty();
    }

    [Fact]
    public void AddTaskLine_ShouldAddLineSuccessfully()
    {
        // Arrange
        var taskHeader = CreateTaskHeader();
        var fromLocationId = Guid.NewGuid();
        var toLocationId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        decimal totalQty = 15.0m;

        // Act
        var result = taskHeader.AddTaskLine(fromLocationId, toLocationId, productId, totalQty);

        // Assert
        result.IsSuccess.Should().BeTrue();
        taskHeader.Lines.Should().HaveCount(1);
        var taskLine = taskHeader.Lines.First();
        taskLine.FromLocationId.Should().Be(fromLocationId);
        taskLine.ToLocationId.Should().Be(toLocationId);
        taskLine.ProductId.Should().Be(productId);
        taskLine.TotalQty.Should().Be(totalQty);
    }

    [Fact]
    public void MapDispatchLineToTaskLine_ShouldMapSuccessfully()
    {
        // Arrange
        var taskHeader = CreateTaskHeader();
        var taskLineResult = taskHeader.AddTaskLine(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 10.0m);
        var taskLine = taskLineResult.Value;
        var dispatchLineId = Guid.NewGuid();
        decimal allocatedQty = 5.0m;

        // Act
        var result = taskHeader.MapDispatchLineToTaskLine(taskLine.Id, dispatchLineId, allocatedQty);

        // Assert
        result.IsSuccess.Should().BeTrue();
        taskLine.DispatchMappings.Should().HaveCount(1);
        var mapping = taskLine.DispatchMappings.First();
        mapping.DispatchLineId.Should().Be(dispatchLineId);
        mapping.AllocatedQty.Should().Be(allocatedQty);
    }

    [Fact]
    public void MapDispatchLineToTaskLine_ShouldFailWhenTaskLineNotFound()
    {
        // Arrange
        var taskHeader = CreateTaskHeader();
        var nonExistentTaskLineId = Guid.NewGuid();
        var dispatchLineId = Guid.NewGuid();

        // Act
        var result = taskHeader.MapDispatchLineToTaskLine(nonExistentTaskLineId, dispatchLineId, 5.0m);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TaskErrors.TaskLineNotFound(nonExistentTaskLineId));
    }

    [Fact]
    public void CompleteTaskLine_ShouldCompleteTaskLineSuccessfully()
    {
        // Arrange
        var taskHeader = CreateTaskHeaderInProgress();
        var taskLineResult = taskHeader.AddTaskLine(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 10.0m);
        var taskLine = taskLineResult.Value;
        decimal completedQty = 10.0m;

        // Act
        var result = taskHeader.CompleteTaskLine(taskLine.Id, completedQty);

        // Assert
        result.IsSuccess.Should().BeTrue();
        taskLine.CompletedQty.Should().Be(completedQty);
        taskLine.IsCompleted.Should().BeTrue();
        // Task should be completed since all lines are completed
        taskHeader.Status.Should().Be(TaskHeaderStatus.Completed);
    }

    [Fact]
    public void CompleteTaskLine_ShouldNotCompleteTaskHeaderWhenNotAllLinesAreCompleted()
    {
        // Arrange
        var taskHeader = CreateTaskHeaderInProgress();
        var taskLine1Result = taskHeader.AddTaskLine(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 10.0m);
        taskHeader.AddTaskLine(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 5.0m);
        var taskLine1 = taskLine1Result.Value;

        // Act
        var result = taskHeader.CompleteTaskLine(taskLine1.Id, 10.0m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        taskLine1.IsCompleted.Should().BeTrue();
        // Task should still be in progress since not all lines are completed
        taskHeader.Status.Should().Be(TaskHeaderStatus.InProgress);
    }

    [Fact]
    public void CompleteTaskLine_ShouldFailWhenTaskNotInProgress()
    {
        // Arrange
        var taskHeader = CreateTaskHeader(); // Not assigned yet
        var taskLineResult = taskHeader.AddTaskLine(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 10.0m);
        var taskLine = taskLineResult.Value;

        // Act
        var result = taskHeader.CompleteTaskLine(taskLine.Id, 10.0m);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TaskErrors.NotInProgress);
    }

    [Fact]
    public void CompleteTaskLine_ShouldFailWhenTaskLineNotFound()
    {
        // Arrange
        var taskHeader = CreateTaskHeaderInProgress();
        var nonExistentTaskLineId = Guid.NewGuid();

        // Act
        var result = taskHeader.CompleteTaskLine(nonExistentTaskLineId, 10.0m);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TaskErrors.TaskLineNotFound(nonExistentTaskLineId));
    }

    [Fact]
    public void FindTaskLineByProductAndLocation_ShouldReturnTaskLineWhenExists()
    {
        // Arrange
        var taskHeader = CreateTaskHeader();
        var fromLocationId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        taskHeader.AddTaskLine(fromLocationId, Guid.NewGuid(), productId, 10.0m);

        // Act
        var foundTaskLine = taskHeader.FindTaskLineByProductAndLocation(productId, fromLocationId);

        // Assert
        foundTaskLine.Should().NotBeNull();
        foundTaskLine?.ProductId.Should().Be(productId);
        foundTaskLine?.FromLocationId.Should().Be(fromLocationId);
    }

    [Fact]
    public void FindTaskLineByProductAndLocation_ShouldReturnNullWhenNotExists()
    {
        // Arrange
        var taskHeader = CreateTaskHeader();
        var nonExistentProductId = Guid.NewGuid();
        var nonExistentLocationId = Guid.NewGuid();

        // Act
        var foundTaskLine = taskHeader.FindTaskLineByProductAndLocation(nonExistentProductId, nonExistentLocationId);

        // Assert
        foundTaskLine.Should().BeNull();
    }

    private static TaskHeader CreateTaskHeader()
    {
        var result = TaskHeader.Create(
            "PICK-001",
            null,
            null,
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateOnly(2025, 5, 25),
            TaskType.PickingItem);

        return result.Value;
    }

    private static TaskHeader CreateTaskHeaderInProgress()
    {
        var taskHeader = CreateTaskHeader();
        
        // Add an assignment and set status to InProgress
        // This is a simplified version - in reality, we would need to call AddAssignment and Start
        // But for testing purposes, we're using reflection to set the status directly
        
        // Using reflection to set the private Status property for testing
        typeof(TaskHeader)
            .GetProperty(nameof(TaskHeader.Status))
            ?.SetValue(taskHeader, TaskHeaderStatus.InProgress);
            
        return taskHeader;
    }
}
