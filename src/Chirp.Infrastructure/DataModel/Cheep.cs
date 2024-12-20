using System.ComponentModel.DataAnnotations;

namespace Chirp.Infrastructure.DataModel;

public class Cheep
{
    public int CheepId { get; set; }
    public required string AuthorId { get; set; }
    public required Author Author { get; set; }
    public required string Text { get; set; }
    public required DateTime TimeStamp { get; set; }
    public DateTime? EditedTimeStamp { get; set; }
}