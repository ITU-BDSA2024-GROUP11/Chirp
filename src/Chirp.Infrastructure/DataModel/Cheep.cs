namespace Chirp.Infrastructure.DataModel;
using System.ComponentModel.DataAnnotations;

public class Cheep
{
    public int CheepId { get; set; }
    public int AuthorId { get; set; }
    public required Author Author { get; set; }

    [MaxLength(160)]
    public required string Text { get; set; }
    public required DateTime TimeStamp { get; set; }
}