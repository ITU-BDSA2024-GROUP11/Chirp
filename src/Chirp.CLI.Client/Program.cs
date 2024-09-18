using Chirp.CLI;
using SimpleDB;
using CommandLine;


static void ReadCheeps()
{
    CsvDatabase<Cheep> db = CsvDatabase<Cheep>.getInstance();
    UserInterface.PrintCheeps(db.Read(0));
}

static void StoreCheep(string message)
{
    CsvDatabase<Cheep> db = CsvDatabase<Cheep>.getInstance();

    var cheep = new Cheep
    (
        Environment.UserName, message, DateTimeOffset.UtcNow.ToUnixTimeSeconds()
    );
    db.Store(cheep);
}


Parser.Default.ParseArguments<ReadOptions, CheepOptions>(args)
    .WithParsed<ReadOptions>(opts => ReadCheeps())
    .WithParsed<CheepOptions>(opts => StoreCheep(opts.Message));


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
    //comment
    [Value(0, MetaName = "message", HelpText = "The message to store.", Required = true)]
    public string Message { get; set; }
}

public record Cheep(string Author, string Message, long Timestamp);