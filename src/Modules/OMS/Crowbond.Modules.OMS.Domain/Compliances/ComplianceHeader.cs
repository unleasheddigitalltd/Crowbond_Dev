using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Compliances;

public sealed class ComplianceHeader : Entity
{
    private readonly List<ComplianceLine> _lines = new();
    private ComplianceHeader()
    {
    }

    public Guid Id { get; private set; }

    public Guid RouteTripLogId { get; private set; }

    public Guid VehicleId { get; private set; }

    public string FormNo { get; private set; }

    public DateOnly FormDate { get; private set; }

    public decimal? Temperature { get; private set; }

    public bool? IsConfirmed { get; private set; }

    public IReadOnlyCollection<ComplianceLine> Lines => _lines;

    public static ComplianceHeader Create(Guid routeTripLogId, Guid vehicleId, string formNo, DateOnly formDate)
    {
        var header = new ComplianceHeader
        {
            Id = Guid.NewGuid(),
            RouteTripLogId = routeTripLogId,
            VehicleId = vehicleId,
            FormNo = formNo,
            FormDate = formDate
        };

        return header;
    }

    public void AddLine(Guid questionId)
    {
        var line = ComplianceLine.Create(questionId);
        _lines.Add(line);
    }

    public Result<bool> Submit(decimal temprature)
    {
        Temperature = temprature;

        if (Lines.Any(l => l.Response is null))
        {
            return Result.Failure<bool>(ComplianceErrors.NotAllQuestionsAnswered);
        }

        IsConfirmed = Lines.All(l => l.Response == true);

        return Result.Success(IsConfirmed.Value);
    }

    public Result UpdateLine(Guid complianceLineId, bool? response, string? description)
    {
        ComplianceLine? line = Lines.SingleOrDefault(l => l.Id == complianceLineId);

        if (line is null)
        {
            return Result.Failure(ComplianceErrors.LineNotFound(complianceLineId));
        }

        line.Update(response, description);
        return Result.Success();
    }
}
