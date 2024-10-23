using System.ComponentModel.DataAnnotations;

namespace Chirp.Core.DTO;

public class CheepDTO
{
    [StringLength(160)]
    public required string Text { get; set; }
    public required string Author { get; set; }
    public required string TimeStamp { get; set; }
}