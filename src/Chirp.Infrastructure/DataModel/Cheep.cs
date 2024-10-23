using System.ComponentModel.DataAnnotations;

namespace Chirp.Infrastructure.DataModel;

public class Cheep
{
    public int CheepId { get; set; }
    public int AuthorId { get; set; }
    public required Author Author { get; set; }
    [StringLength(160)]
    public required string Text { get; set; }
    public DateTime TimeStamp { get; set; }
}