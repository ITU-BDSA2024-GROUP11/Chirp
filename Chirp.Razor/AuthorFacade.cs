using System.Data;
using Microsoft.Data.Sqlite;

var sqlDBFilePath = "/tmp/chirp.db";
var AuthorQuery = "SELECT * FROM message, user" + "WHERE message.author_id = user.user_id and user.username = @Author;";
// ORDER by message.pub_date desc (order stuff)
string author = "Adrian"; //Has to be a call from a method that takes input from user
using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
{
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText = AuthorQuery;
    command.Parameters.AddWithValue("@Author", author);

    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
    
    }
}