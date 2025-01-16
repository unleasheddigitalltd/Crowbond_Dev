using System.Numerics;
using Bogus;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;
using Crowbond.Modules.WMS.UnitTests.Abstractions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace Crowbond.Modules.WMS.UnitTests.Tasks;

public class TaskTests : BaseTest
{
    [Fact]
    public void Create_ShouldReturnNewTaskHeader_WhenInputIsValid()
    {
        // Arrange
        string taskNo = Faker.Random.AlphaNumeric(10);
        var receiptId = Guid.NewGuid();
        var dispatchId = Guid.NewGuid();
        TaskType taskType = Faker.PickRandom<TaskType>();

        // Act
        Result<TaskHeader> result = TaskHeader.Create(taskNo, receiptId, dispatchId, taskType);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.TaskNo.Should().Be(taskNo);
        result.Value.ReceiptId.Should().Be(receiptId);
        result.Value.DispatchId.Should().Be(dispatchId);
        result.Value.TaskType.Should().Be(taskType);
        result.Value.Status.Should().Be(TaskHeaderStatus.NotAssigned);
        result.Value.Assignments.Should().BeEmpty();
    }

    //Tests for AddAssignment
    [Fact]
    public void AddAssignment_ShouldAddAssignment_WhenWarehouseOperatorIsValid()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        var warehouseOperatorId = Guid.NewGuid();

        // Act
        Result<TaskAssignment> result = taskHeader.AddAssignment(warehouseOperatorId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.AssignedOperatorId.Should().Be(warehouseOperatorId);
        taskHeader.Status.Should().Be(TaskHeaderStatus.Assigned);
        taskHeader.Assignments.Should().HaveCount(1);
    }

    [Fact]
    public void AddAssignment_ShouldReturnFailure_WhenTaskHeaderNotInNotAssignedState()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        taskHeader.AddAssignment(Guid.NewGuid());

        // Act
        Result<TaskAssignment> result = taskHeader.AddAssignment(Guid.NewGuid());

        // Assert
        result.Error.Should().Be(TaskErrors.NotAvailableForAssignment);
    }

    [Fact]
    public void AddAssignment_ShouldReturnFailure_WhenExistingInProgressAssignment()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        taskHeader.AddAssignment(Guid.NewGuid());
        taskHeader.Start(DateTime.UtcNow);

        // Act
        Result<TaskAssignment> secondAssignmentResult = taskHeader.AddAssignment(Guid.NewGuid());

        // Assert
        secondAssignmentResult.Error.Should().Be(TaskErrors.NotAvailableForAssignment);
    }

    //Tests for Start
    [Fact]
    public void Start_ShouldStartAssignment_WhenModificationDateIsValid()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        var warehouseOperatorId = Guid.NewGuid();
        taskHeader.AddAssignment(warehouseOperatorId);

        DateTime modificationDate = Faker.Date.Recent();

        // Act
        Result startResult = taskHeader.Start(modificationDate);

        // Assert
        startResult.IsSuccess.Should().BeTrue();
        taskHeader.Status.Should().Be(TaskHeaderStatus.InProgress);
        TaskAssignment? startedAssignment = taskHeader.Assignments.FirstOrDefault();
        startedAssignment.Should().NotBeNull();
        startedAssignment!.Status.Should().Be(TaskAssignmentStatus.InProgress);
        startedAssignment.StartDateTime.Should().Be(modificationDate);

    }

    [Fact]
    public void Start_ShouldReturnFailure_WhenTaskHeaderNotInAssignedState()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        DateTime modificationDate = Faker.Date.Recent();

        // Act
        Result startResult = taskHeader.Start(modificationDate);

        // Assert
        startResult.Error.Should().Be(TaskErrors.NotAssigned);
    }


    //Tests for Pause
    [Fact]
    public void Pause_ShouldPauseAndReturnSuccess_WhenHasValidInProgressAssignment()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        taskHeader.AddAssignment(Guid.NewGuid());

        taskHeader.Start(DateTime.UtcNow);

        // Act
        Result pauseResult = taskHeader.Pause();

        // Assert
        pauseResult.IsSuccess.Should().BeTrue();
        taskHeader.Status.Should().Be(TaskHeaderStatus.InProgress);
        TaskAssignment? pausedAssignment = taskHeader.Assignments.FirstOrDefault();
        pausedAssignment.Should().NotBeNull();
        pausedAssignment!.Status.Should().Be(TaskAssignmentStatus.Paused);
    }

    [Fact]
    public void Pause_ShouldReturnFailure_WhenTaskHeaderNotInInProgressState()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        // Act
        Result pauseResult = taskHeader.Pause();

        // Assert
        pauseResult.Error.Should().Be(TaskErrors.NotInProgress);
    }

    [Fact]
    public void Pause_ShouldReturnFailure_WhenHasNoInProgressAssignment()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        taskHeader.AddAssignment(Guid.NewGuid());
        taskHeader.Start(DateTime.UtcNow);
        taskHeader.Pause();

        // Act
        Result pauseResult = taskHeader.Pause();

        // Assert
        pauseResult.Error.Should().Be(TaskErrors.HasNoInprogressAssignmet(taskHeader.Id));
    }

    //Tests for Unpause
    [Fact]
    public void Unpause_ShouldUnpause_WhenHasValidPausedAssignment()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        taskHeader.AddAssignment(Guid.NewGuid());
        taskHeader.Start(DateTime.UtcNow);
        taskHeader.Pause();

        // Act
        Result unpauseResult = taskHeader.Unpause();

        // Assert
        unpauseResult.IsSuccess.Should().BeTrue();
        taskHeader.Status.Should().Be(TaskHeaderStatus.InProgress);
        TaskAssignment? unpausedAssignment = taskHeader.Assignments.FirstOrDefault();
        unpausedAssignment.Should().NotBeNull();
        unpausedAssignment!.Status.Should().Be(TaskAssignmentStatus.InProgress);
    }

    [Fact]
    public void Unpause_ShouldReturnFailure_WhenTaskHeaderNotInInProgressState()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        // Act
        Result unpauseResult = taskHeader.Unpause();

        // Assert
        unpauseResult.Error.Should().Be(TaskErrors.NotInProgress);
    }

    [Fact]
    public void Unpause_ShouldReturnFailure_WhenHasNoPausedAssignment()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        taskHeader.AddAssignment(Guid.NewGuid());
        taskHeader.Start(DateTime.UtcNow);

        // Act
        Result unpauseResult = taskHeader.Unpause();

        // Assert
        unpauseResult.Error.Should().Be(TaskErrors.HasNoPausedAssignmet(taskHeader.Id));
    }

    //Tests for Quit
    [Fact]
    public void Quit_ShouldQuit_WhenHasValidInProgressAssignment()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        taskHeader.AddAssignment(Guid.NewGuid());
        taskHeader.Start(Faker.Date.Recent());

        DateTime modificationDate = DateTime.UtcNow;

        // Act
        Result quitResult = taskHeader.Quit(modificationDate);

        // Assert
        quitResult.IsSuccess.Should().BeTrue();
        taskHeader.Status.Should().Be(TaskHeaderStatus.NotAssigned);
        TaskAssignment? quitedAssignment = taskHeader.Assignments.FirstOrDefault();
        quitedAssignment.Should().NotBeNull();
        quitedAssignment!.Status.Should().Be(TaskAssignmentStatus.Quit);
        quitedAssignment.EndDateTime.Should().Be(modificationDate);
    }

    [Fact]
    public void Quit_ShouldReturnFailure_WhenTaskHeaderNotInInProgressState()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        // Act
        Result quitResult = taskHeader.Quit(DateTime.Now);

        // Assert
        quitResult.Error.Should().Be(TaskErrors.NotInProgress);
    }

    //Tests for Complete
    [Fact]
    public void Complete_ShouldComplete_WhenInProgressAssignmentIsValid()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        taskHeader.AddAssignment(Guid.NewGuid());
        taskHeader.Start(DateTime.Now);
        DateTime modificationDate = DateTime.UtcNow;

        // Act
        Result result = taskHeader.Complete(modificationDate);

        // Assert
        result.IsSuccess.Should().BeTrue();
        taskHeader.Status.Should().Be(TaskHeaderStatus.Completed);
        taskHeader.Assignments.First().Status.Should().Be(TaskAssignmentStatus.Completed);
    }

    [Fact]
    public void Complete_ShouldReturnFailure_WhenTaskHeaderNotInInProgressState()
    {
        // Arrange
        TaskHeader taskHeader = TaskHeader.Create(
            Faker.Random.AlphaNumeric(10),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.PickRandom<TaskType>()).Value;

        // Act
        Result result = taskHeader.Complete(DateTime.UtcNow);

        // Assert
        result.Error.Should().Be(TaskErrors.NotInProgress);
    }   
}
