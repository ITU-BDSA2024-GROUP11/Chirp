namespace Chirp.Razor.Tests;

public class EFCore
{
    [Fact]
    public void CheepTest()
    {
        var timestamp = DateTime.Now;
        Cheep cheep = new Cheep("Hello", timestamp, 1, 1);
        Assert.Equal("Hello", cheep.Text);
        Assert.Equal(timestamp, cheep.TimeStamp);
        Assert.Equal(1, cheep.AuthorID);
        Assert.Equal(1, cheep.ID);
    }
}