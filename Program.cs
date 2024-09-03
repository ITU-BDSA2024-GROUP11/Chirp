using System.IO;
using System;

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
    //Pass the file path and file name to the StreamReader constructor
    StreamReader sr = new StreamReader("./chirp_cli_db.csv");
    //Read the first line of text
    String line = sr.ReadLine();
    //Continue to read until you reach end of file
    while (line != null)
    {
        if (line.Equals("Author  Message    Timestamp"))
        {
            line = sr.ReadLine();
            continue;
        }

        //Splits by the first comma
        int firstCommaIndex = line.IndexOf(',');
        //Splits by the last comma
        int lastCommaIndex = line.LastIndexOf(',');
        
        string user = line.Substring(0, firstCommaIndex);
        string message = line.Substring(firstCommaIndex + 1, lastCommaIndex - firstCommaIndex - 1).Replace("\"","");
        string unixTime = line.Substring(lastCommaIndex + 1);

        //Converts string from file to int
        int i = 0;
        int.TryParse(unixTime, out i);
        string timeInDate = (UnixToDate(i));

        Console.WriteLine(user + " @ " + timeInDate + ": " + message);
        //Read the next line
        line = sr.ReadLine();
    }
    //close the file
    sr.Close();
}

static void writeToFile(string message)
{
    using (StreamWriter sw = new StreamWriter("./chirp_cli_db.csv", append: true))
    {
        //Goes to next line
        sw.WriteLine();
        string username = Environment.UserName;
        long unixTime=DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        //Writes in datafile
        sw.Write(username + ",\"" + message + "\"," + unixTime);
        sw.Close();
    }
}

static String UnixToDate(Int32 unixTime)
{
    return DateTimeOffset.FromUnixTimeSeconds(unixTime).ToLocalTime().DateTime.ToString("MM/dd/yy HH:mm:ss").Replace('.',':');

}