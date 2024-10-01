using Microsoft.Data.Sqlite;

public class AuthorFacade
{
    string dbpath = Environment.GetEnvironmentVariable("CHIRPDBPATH");
    string AuthorQuery = @"SELECT username, text, pub_date FROM message, user WHERE author_id = user_id and username = @Author ORDER by message.pub_date desc";
    string AllCheepsQuery = @"SELECT username, text, pub_date FROM message, user WHERE author_id = user_id ORDER by message.pub_date desc";
    SqliteConnection connection;

// ORDER by message.pub_date desc (order stuff)
    public AuthorFacade()
    {
        if (dbpath == null){
            //dbpath = "/tmp/chirp.db";
            /*
            string localdbpath = Path.GetTempPath();
            //Console.WriteLine(localdbpath);
            string fullPath = Path.Combine(localdbpath, "chirp.db");
            // Create the file
            Console.WriteLine(fullPath);
            /*using (FileStream fs = File.Create(fullPath))
            {
                Console.WriteLine("File created successfully at: " + fullPath);
            }*/
        }
        
        using (connection = new SqliteConnection($"Data Source={dbpath}"))
        {
            connection.Open();
        }
    }

    public List<CheepViewModel> GetCheeps()
    {
        connection.Open();
        var cheeps = new List<CheepViewModel>();
        using var command = new SqliteCommand(AllCheepsQuery, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            cheeps.Add(new CheepViewModel(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
        }
        return cheeps;
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author)
    {
        connection.Open();
        using var command = new SqliteCommand(AuthorQuery, connection);
        command.Parameters.Add("@Author", SqliteType.Text); 
        command.Parameters["@Author"].Value = author;
        Console.WriteLine("connection is " + connection.State);
        Console.WriteLine(command.CommandText);
        using var reader = command.ExecuteReader();
        var cheeps = new List<CheepViewModel>();
        while (reader.Read()) 
        { 
            cheeps.Add(new CheepViewModel(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
        }

        return cheeps;
    }
}