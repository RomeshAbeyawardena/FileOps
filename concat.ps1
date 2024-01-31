param (
	[string]$inputFilePath,
	[string]$output
)

 if ([String]::IsNullOrWhiteSpace($inputFilePath)) {
	$inputFilePath = $PWD
 }

 $inputDirectory = [System.IO.DirectoryInfo]::new($inputFilePath);

 $csfiles = $inputDirectory.GetFiles("*.cs", [System.IO.SearchOption]::AllDirectories);
 $csProjfiles = $inputDirectory.GetFiles("*.csproj", [System.IO.SearchOption]::AllDirectories);

$files = $csfiles + $csProjfiles

 $stringBuilder = [System.Text.StringBuilder]::new();

 $EOF = [String]::new('-', 4) + "END OF FILE" + [String]::new('-', 4);
 foreach($file in $files) {
	$parentName = $file.Directory.Name;
	$fileName = $file.Name;
	$stringBuilder.AppendLine([String]::new('-', 4) + "$parentName/$fileName" + [String]::new('-', 4));

	$stringBuilder.AppendLine([System.IO.File]::ReadAllText($file.FullName));
	$stringBuilder.AppendLine($EOF);
 }

 [System.IO.File]::WriteAllLines("$PWD/$output", $stringBuilder.ToString());