namespace Chirp.Razor.Tests;

public class EFCoreTest
{
    [Fact]
    public void CheepTest()
    {
        var timestamp = DateTime.Now;
        Cheep cheep = new Cheep() { Text = "Hello", TimeStamp = timestamp, AuthorId = 1, Id = 1 };
        Assert.Equal("Hello", cheep.Text);
        Assert.Equal(timestamp, cheep.TimeStamp);
        Assert.Equal(1, cheep.AuthorId);
        Assert.Equal(1, cheep.Id);
    }

    [Fact]
    public void AuthorTest()
    {
        Author author = new Author() { Name = "John Doe", Email = "jd@gmail.com", Id = 1, Cheeps = new List<Cheep>() };
        Assert.Equal("John Doe", author.Name);
        Assert.Equal("jd@gmail.com", author.Email);
        Assert.Equal(1, author.Id);
    }
    
    [Fact]
    public void AuthorCheepTest()
    {
        var timestamp = DateTime.Now;
        Author author = new Author() { Name = "John Doe", Email = "jd@gmail.com", Id = 1, Cheeps = new List<Cheep>() };
        Cheep cheep = new Cheep() {AuthorId=1, Id = 1, Text = "Hello", TimeStamp = timestamp};
        author.addCheep(cheep);
        Assert.Single(author.Cheeps);
        Assert.Equal(cheep, author.Cheeps[0]);
    }
}