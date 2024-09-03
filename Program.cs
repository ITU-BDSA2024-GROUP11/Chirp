using System.IO;
using System;

if (args[0] == "read")
{
    readFile();
}
else if (args[0] == "cheep")
{
    writeToFile(args[1]);
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
        if (line.Equals("Author  Message	Timestamp"))
        {
            line = sr.ReadLine();
            continue;
        }

        //write the line to console window
        string[] words = line.Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var user = (words[0]);


        var message = string.Join(" ", words, 1, words.Length - 2);

        int i = 0;
        string s = words[words.Length - 1];
        int.TryParse(s, out i);
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
        sw.WriteLine();
        string username = Environment.UserName;
        //sw.WriteLine(content);
        long unixTime=DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        sw.Write(username + "\t" + message + "\t" + unixTime);
        sw.Close();
    }
}

static String UnixToDate(Int32 unixTime)
{
    return DateTimeOffset.FromUnixTimeSeconds(unixTime).ToLocalTime().DateTime.ToString("MM/dd/yy HH:mm:ss").Replace('.',':');
    
}

