namespace Chirp.CLI;

public static class UserInterface
{
    public static void PrintCheeps(IEnumerable<Cheep> cheeps)
    {
        foreach (var cheep in cheeps)
        {
            string timeInDate = UnixToDate(cheep.Timestamp);
            Console.WriteLine($"{cheep.Author} @ {timeInDate}: {cheep.Message}");
        }
    }
    
    static String UnixToDate(long unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).ToLocalTime().DateTime.ToString("MM/dd/yy HH:mm:ss").Replace('.',':');

    }
}