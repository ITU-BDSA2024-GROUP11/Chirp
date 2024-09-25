using Microsoft.Data.Sqlite;

public class AuthorFacade
{
    string sqlDBFilePath = "/tmp/chirp.db";

    string AuthorQuery = "SELECT * FROM message, user" +
                         "WHERE message.author_id = user.user_id and user.username = @Author";
    SqliteConnection connection;

// ORDER by message.pub_date desc (order stuff)
    public AuthorFacade()
    {
        using (connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open();
        }
    }

    public void GetCheepsFromAuthor(string author)
    {
        var command = connection.CreateCommand(); 
        command.CommandText = AuthorQuery;
        
        using var reader = command.ExecuteReader(); 
        while (reader.Read())
        {
            
        }
    }
}