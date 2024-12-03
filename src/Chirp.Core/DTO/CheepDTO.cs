namespace Chirp.Core.DTO;

public class CheepDTO
{
    public required int CheepId { get; set; }
    public required string Text { get; set; }
    public required string Author { get; set; }
    public required DateTime TimeStamp { get; set; }
}