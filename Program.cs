using System.Globalization;
using CsvHelper;

if (args[0] == "read")
{
    readFile();
}
else if (args[0] == "cheep")
{
    //Gets the message and defines it as inputString
    string inputString = string.Join(" ", args.Skip(1).ToArray());
    writeToFile(inputString);
}

static void readFile()
{
    using (var reader = new StreamReader("./chirp_cli_db.csv"))
    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    {
        // Reads the records directly into the Cheep record
        var records = csv.GetRecords<Cheep>().ToList();
        
        foreach (var cheep in records)
        {
            string timeInDate = UnixToDate(cheep.Timestamp);
            Console.WriteLine($"{cheep.Author} @ {timeInDate}: {cheep.Message}");
        }
    }
}

static void writeToFile(string message)
{

    var cheep = new Cheep
    {
        Author = Environment.UserName, Message = message, Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
    };
    
    using (var writer = new StreamWriter("./chirp_cli_db.csv", append: true))
    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    {
        writer.WriteLine();
        csv.WriteRecord(cheep);
    }
}

static String UnixToDate(long unixTime)
{
    return DateTimeOffset.FromUnixTimeSeconds(unixTime).ToLocalTime().DateTime.ToString("MM/dd/yy HH:mm:ss").Replace('.',':');

}

public class Cheep
{
    public string Author { get; set; }
    public string Message { get; set; }
    public long Timestamp { get; set; }
}
