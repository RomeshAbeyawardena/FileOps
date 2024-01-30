using System.Collections;
using System.Text;

namespace FileOps.Core;

public class OperationLedger : IEnumerable<OperationLedgerEntry>
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

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        foreach(var entry in ledgerEntries) 
        {
            stringBuilder.AppendLine(entry.ToString());
        }
        return stringBuilder.ToString();
    }
}