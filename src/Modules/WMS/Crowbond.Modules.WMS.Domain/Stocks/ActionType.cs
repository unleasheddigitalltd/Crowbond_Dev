﻿using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Stocks;

public sealed class ActionType : Entity
{
    public static readonly ActionType Adjustment = new("Adjustment");

    public ActionType(string name)
    {
        Name = name;
    }

    public ActionType()
    {
    }

    public string Name { get; private set; }
}
