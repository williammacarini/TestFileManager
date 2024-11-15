public class FileOrganizer
{
    public void OrganizeFiles(string sourceDirectory, string destinationDirectory)
    {
        if (!Directory.Exists(sourceDirectory))
        {
            return;
        }

        Directory.CreateDirectory(destinationDirectory);
        string[] files = Directory.GetFiles(sourceDirectory);

        foreach (var file in files)
        {
            string extension = Path.GetExtension(file).ToLower();
            string subFolder = Path.Combine(destinationDirectory, extension.TrimStart('.'));

            Directory.CreateDirectory(subFolder);

            string destFile = Path.Combine(subFolder, Path.GetFileName(file));
            File.Move(file, destFile);
        }
    }
}

public class FileAnalyser
{
    public Dictionary<string, int> AnalyzeExtensions(string directory)
    {
        var extensionsCounts = new Dictionary<string, int>();

        if (!Directory.Exists(directory))
        {
            return extensionsCounts;
        }

        string[] files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            string extension = Path.GetExtension(file).ToLower();

            if (extensionsCounts.ContainsKey(extension))
                extensionsCounts[extension]++;
            else
                extensionsCounts[extension] = 1;
        }

        return extensionsCounts;
    }
}

public class Logger
{
    private readonly string _logFilePath = "activy-log.txt";

    public Logger()
    {

    }

    public Logger(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    public void LogActivity(string message)
    {
        string log = $"{DateTime.Now}: {message}";

        using (StreamWriter writer = new StreamWriter(_logFilePath, true))
        {
            writer.WriteLine(message);
        }
    }
}

public class Program
{
    public static void Main()
    {
        string sourceDirectory = @"C:\Users\william.macarini\Downloads\Source";
        string destinationDirectory = @"C:\Users\william.macarini\Downloads\Destination";
        string logFilePath = @"C:\Users\william.macarini\Downloads\Log";

        var organizer = new FileOrganizer();
        var analyzer = new FileAnalyser();
        var logger = new Logger(logFilePath);

        organizer.OrganizeFiles(sourceDirectory, destinationDirectory);

        var extensionsCounts = analyzer.AnalyzeExtensions(destinationDirectory);
        foreach (var extensions in extensionsCounts)
        {
            Console.WriteLine($"{extensions.Key}: {extensions.Value}");
        }

        logger.LogActivity("Extension Analyse of the files is done");
    }
}
