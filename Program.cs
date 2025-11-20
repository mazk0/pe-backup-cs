using PEBackup;

var (isValid, parsedArgs, errorMessage) = ConsoleInputValidation.ParseAndValidate(Environment.GetCommandLineArgs());

if (!isValid)
{
    Console.WriteLine(errorMessage);
    ConsoleInputValidation.WriteExample();
    return;
}

var destination = ConsoleInputValidation.AppendDestination(parsedArgs!.Destination, parsedArgs.IncludeDate);

Console.WriteLine($"Downloading data form Source: {parsedArgs.Source} to Destination: {destination}");
await BackupService.Backup(parsedArgs.Source.ToString(), destination);
Console.WriteLine("Download finished");
