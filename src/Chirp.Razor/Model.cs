using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor;

public class Cheep
{
    public string Text; 
    public DateTime TimeStamp;
    public int AuthorID;
    public int ID;
    
    public Cheep(string text, DateTime timeStamp, int authorID, int id)
    {
        Text = text;
        TimeStamp = timeStamp;
        AuthorID = authorID;
        ID = id;
    }
    
}
