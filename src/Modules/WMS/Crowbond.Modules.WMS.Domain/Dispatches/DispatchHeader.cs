﻿using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Dispatches;

public sealed class DispatchHeader : Entity, IAuditable
{
    private readonly List<DispatchLine> _lines = new();

    private DispatchHeader()
    {        
    }

    public Guid Id { get; private set; }

    public string DispatchNo { get; private set; }

    public Guid RouteTripId { get; private set; }

    public DateOnly RouteTripDate { get; private set; }

    public string RouteName { get; private set; }

    public DispatchStatus Status { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public IReadOnlyCollection<DispatchLine> Lines => _lines;


    public static DispatchHeader Create(
        string dispatchNo,
        Guid routeTripId,
        DateOnly routeTripDate,
        string routeName)
    {
        var header = new DispatchHeader
        {
            Id = Guid.NewGuid(),
            DispatchNo = dispatchNo,
            RouteTripId = routeTripId,
            RouteTripDate = routeTripDate,
            RouteName = routeName,
            Status = DispatchStatus.NotStarted
        };

        return header;
    }

    public DispatchLine AddLine(
        Guid orderId,
        string orderNo,
        string customerBusinessName,
        Guid orderLineId,
        Guid productId,
        decimal orderedQty)
    {
        var line = DispatchLine.Create(
            orderId,
            orderNo,
            customerBusinessName,
            orderLineId,
            productId, 
            orderedQty);
        _lines.Add(line);
        return line;
    }
}
