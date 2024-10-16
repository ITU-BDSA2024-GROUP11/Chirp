using Chirp.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Tests;

public class EFCoreTest
{
    [Fact]
    public void CheepTest()
    {
        var timestamp = DateTime.Now;
        var cheep = new Cheep { Text = "Hello", TimeStamp = timestamp, AuthorId = 1, CheepId = 1 };
        Assert.Equal("Hello", cheep.Text);
        Assert.Equal(timestamp, cheep.TimeStamp);
        Assert.Equal(1, cheep.AuthorId);
        Assert.Equal(1, cheep.CheepId);
    }

    [Fact]
    public void AuthorTest()
    {
        var author = new Author { Name = "John Doe", Email = "jd@gmail.com", AuthorId = 1, Cheeps = new List<Cheep>() };
        Assert.Equal("John Doe", author.Name);
        Assert.Equal("jd@gmail.com", author.Email);
        Assert.Equal(1, author.AuthorId);
    }

    [Fact]
    public void AuthorCheepTest()
    {
        var timestamp = DateTime.Now;
        var author = new Author { Name = "John Doe", Email = "jd@gmail.com", AuthorId = 1, Cheeps = new List<Cheep>() };
        var cheep = new Cheep { AuthorId = 1, CheepId = 1, Text = "Hello", TimeStamp = timestamp };
        author.AddCheep(cheep);
        Assert.Single(author.Cheeps);
        Assert.Equal(cheep, author.Cheeps[0]);
    }

    [Fact]
    public void DBTest()
    {
        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .Options;
        using (var context = new ChirpDBContext(options))
        {
        }
    }
}