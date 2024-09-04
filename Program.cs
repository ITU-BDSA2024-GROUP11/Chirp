using System.Globalization;
using CsvHelper;
using SimpleDB;

if (args[0] == "read")
{
    ReadCheeps();
}
else if (args[0] == "cheep")
{
    //Gets the message and defines it as inputString
    string inputString = string.Join(" ", args.Skip(1).ToArray());
    StoreCheep(inputString);
}

static void ReadCheeps()
{ 
        CsvDatabase<Cheep> db = new CsvDatabase<Cheep>();
        
        foreach (var cheep in db.Read(0))
        {
            string timeInDate = UnixToDate(cheep.Timestamp);
            Console.WriteLine($"{cheep.Author} @ {timeInDate}: {cheep.Message}");
        }
}

static void StoreCheep(string message)
{
    CsvDatabase<Cheep> db = new CsvDatabase<Cheep>();

    var cheep = new Cheep
    {
        Author = Environment.UserName, Message = message, Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
    };
    
    db.Store(cheep);
    
    
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
