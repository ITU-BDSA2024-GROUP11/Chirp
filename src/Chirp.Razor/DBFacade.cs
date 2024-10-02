using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.FileProviders;

public class DBFacade
{
    string dbpath = Environment.GetEnvironmentVariable("CHIRPDBPATH");
    string AuthorQuery = @"SELECT username, text, pub_date FROM message, user WHERE author_id = user_id and username = @Author ORDER by message.pub_date desc";
    string AllCheepsQuery = @"SELECT username, text, pub_date FROM message, user WHERE author_id = user_id ORDER by message.pub_date desc";
    string PageQuery = @"SELECT username, text, pub_date FROM message, user WHERE author_id = user_id ORDER by message.pub_date desc LIMIT 32 OFFSET @PageOffset";
    SqliteConnection connection;

// ORDER by message.pub_date desc (order stuff)
    public DBFacade()
    {
        if (dbpath == null)
        {
            // Set a default database path
            Console.WriteLine("Set DbPath to default");
            dbpath = "./tmp/chirp.db";
        } else {
            Console.WriteLine(dbpath);
        }
        
        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
        using var embeddedReader = embeddedProvider.GetFileInfo("data/schema.sql").CreateReadStream();
        using var sr = new StreamReader(embeddedReader);
        var query = sr.ReadToEnd();
        
        using var embeddedReader1 = embeddedProvider.GetFileInfo("data/dump.sql").CreateReadStream();
        using var sr1 = new StreamReader(embeddedReader1);
        var query1 = sr1.ReadToEnd();

        using (connection = new SqliteConnection($"Data Source={dbpath}"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
            
            var command1 = connection.CreateCommand();
            command1.CommandText = query1;
            command1.ExecuteNonQuery();
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
            cheeps.Add(new CheepViewModel(reader.GetString(0), reader.GetString(1), UnixTimeStampToDateTimeString(reader.GetDouble(2))));
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
            cheeps.Add(new CheepViewModel(reader.GetString(0), reader.GetString(1), UnixTimeStampToDateTimeString(reader.GetDouble(2))));
        }

        return cheeps;
    }
    
    public List<CheepViewModel> GetCheepsFromPage(int Page)
    {
        connection.Open();
        using var command =  new SqliteCommand(PageQuery, connection);
        command.Parameters.AddWithValue("@PageOffset", (Page-1)*32);
        using var reader = command.ExecuteReader();
        var cheeps = new List<CheepViewModel>();
        while(reader.Read())
        {
            cheeps.Add(new CheepViewModel(reader.GetString(0), reader.GetString(1), UnixTimeStampToDateTimeString(reader.GetDouble(2))));
        }
        return cheeps;
    }
    
    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }

    public string getDbPath()
    {
        return dbpath;
    }
}