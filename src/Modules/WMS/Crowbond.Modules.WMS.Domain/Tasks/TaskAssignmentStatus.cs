﻿namespace Crowbond.Modules.WMS.Domain.Tasks;

public enum TaskAssignmentStatus
{
    None = 0,
    InProgress = 1,
    Paused = 2,
    Quit = 3,
    Completed = 4,
}
