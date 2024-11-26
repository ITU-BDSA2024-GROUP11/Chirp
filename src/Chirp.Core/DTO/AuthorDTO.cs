namespace Chirp.Core.DTO;

public class AuthorDTO
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Id { get; set; }
    public List<AuthorDTO> Follows { get; set; } = [];
}