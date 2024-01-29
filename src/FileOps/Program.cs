using FileOps.Core;

var op = new FileSystemDirectoryOperation();
var r = await op.CreateDirectoryAsync("C:\\Dev\\ext\\pvt\\FileOps\\test", CancellationToken.None);

Console.WriteLine(r);