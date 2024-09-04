using System.Globalization;
using Chirp.CLI;
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
        UserInterface.PrintCheeps(db.Read(0));
        
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

public class Cheep
{
    public string Author { get; set; }
    public string Message { get; set; }
    public long Timestamp { get; set; }
}
