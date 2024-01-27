using System.Collections;

namespace FileOps.Core.Features.Parse.Operations;

internal class OperationLedger : IEnumerable<OperationLedgerEntry>
{
    private readonly IList<OperationLedgerEntry> ledgerEntries;
    
    public OperationLedger()
    {
        ledgerEntries = new List<OperationLedgerEntry>();
    }

    public void Add(OperationLedgerEntry entry)
    {
        ledgerEntries.Add(entry);
    }

    public IEnumerator<OperationLedgerEntry> GetEnumerator()
    {
        return ledgerEntries.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ledgerEntries.GetEnumerator();
    }
}