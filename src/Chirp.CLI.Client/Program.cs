using Chirp.CLI;
using SimpleDB;
using CommandLine;
using System.Net.Http.Json;
var BaseUri = "http://localhost:5291";
using HttpClient client = new();
client.BaseAddress = new Uri(BaseUri);

async void ReadCheeps()
{
    var cheeps = await client.GetFromJsonAsync<List<Cheep>>("/cheeps");
    UserInterface.PrintCheeps(cheeps);
}

void StoreCheep(string message)
{
    /*
    CsvDatabase<Cheep> db = CsvDatabase<Cheep>.getInstance();
    */
    var cheep = new Cheep
    (
        Environment.UserName, message, DateTimeOffset.UtcNow.ToUnixTimeSeconds()
    );
    
    client.PostAsJsonAsync("/cheep",cheep);
}


Parser.Default.ParseArguments<ReadOptions, CheepOptions>(args)
    .WithParsed<ReadOptions>(opts => ReadCheeps())
    .WithParsed<CheepOptions>(opts => StoreCheep(opts.Message));

    //This is so that the commando prompt doesn't close
    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine();


// Options for the 'read' command
[Verb("read", HelpText = "Read all stored cheeps.")]
class ReadOptions
{
    // No additional properties needed for the 'read' command
}

// Options for the 'cheep' command
[Verb("cheep", HelpText = "Store a new cheep message.")]


class CheepOptions
{
    [Value(0, MetaName = "message", HelpText = "The message to store.", Required = true)]
    public string Message { get; set; }
}

public record Cheep(string Author, string Message, long Timestamp);