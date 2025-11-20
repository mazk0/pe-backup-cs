namespace PEBackup;

public record ParsedArguments(Uri Source, string Destination, bool IncludeDate);

public static class ConsoleInputValidation
{
    public static (bool IsValid, ParsedArguments? Args, string? ErrorMessage) ParseAndValidate(string[] arguments)
    {
        if (arguments.Length < 3)
        {
            return (false, null, "Not enough arguments");
        }

        if (!Uri.TryCreate(arguments[1], UriKind.Absolute, out var sourceUri))
        {
            return (false, null, "Source is not a valid URI");
        }

        var destination = arguments[2];
        var includeDate = false;
        if (arguments.Length > 3 && bool.TryParse(arguments[3], out var addDate))
        {
            includeDate = addDate;
        }

        var parsedArgs = new ParsedArguments(sourceUri, destination, includeDate);
        return (true, parsedArgs, null);
    }
    
    public static string AppendDestination(string destination, bool includeDate)
    {
        if (includeDate)
        {
            var lastDotIndex = destination.LastIndexOf('.');
            return lastDotIndex == -1 
                ? $"{destination}_{DateTime.Now:yyyy-MM-dd}" 
                : destination.Insert(lastDotIndex, $"_{DateTime.Now:yyyy-MM-dd}");
        }

        return destination;
    }
    
    public static void WriteExample()
    {
        Console.WriteLine("Usage: dotnet run <source:string> <destination:string> <IncludeDateInFileName:bool (optional)>");
        Console.WriteLine("Example: dotnet run --configuration Release https://pe.makra.dev/api/event/getall /Volumes/Temp/PolisenSeEvents_Backup/backup.json true");
    }
}