using Shared;

namespace FileOps.Core;

public record OperationLedgerEntry(IClockProvider ClockProvider)
{
    private DateTimeOffset? loggedUtcDate;
    public DateTimeOffset LoggedUtcDate { get => loggedUtcDate 
            ?? (loggedUtcDate = ClockProvider.UtcTime ?? throw new NullReferenceException()).Value; }
    public IOperationConfiguration? Configuration { get; init; }
    public object? Result { get; init; }
    public bool Succeeded { get; init; }
    public Exception? Exception { get; init; }
}
