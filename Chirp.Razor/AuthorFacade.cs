using Microsoft.Data.Sqlite;

public class AuthorFacade
{
    string sqlDBFilePath = "/tmp/chirp.db";

    string AuthorQuery = @"SELECT username, text, pub_date FROM message, user WHERE author_id = user_id and username = @Author ORDER by message.pub_date desc";
    string AllCheepsQuery = @"SELECT username, text, pub_date FROM message, user WHERE author_id = user_id ORDER by message.pub_date desc";
    SqliteConnection connection;

// ORDER by message.pub_date desc (order stuff)
    public AuthorFacade()
    {
        using (connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open();
        }
    }

    public List<Cheeps> GetCheeps()
    {
        connection.Open();
        var cheeps = new List<Cheeps>();
        using var command = new SqliteCommand(AllCheepsQuery, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            cheeps.Add(new Cheeps(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
        }
        return cheeps;
    }

    public List<Cheeps> GetCheepsFromAuthor(string author)
    {
        connection.Open();
        using var command = new SqliteCommand(AuthorQuery, connection);
        command.Parameters.Add("@Author", SqliteType.Text); 
        command.Parameters["@Author"].Value = author;
        Console.WriteLine("connection is " + connection.State);
        Console.WriteLine(command.CommandText);
        using var reader = command.ExecuteReader();
        var cheeps = new List<Cheeps>();
        while (reader.Read()) 
        { 
            cheeps.Add(new Cheeps(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
        }

        return cheeps;
    }
}