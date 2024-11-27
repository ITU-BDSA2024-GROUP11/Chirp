namespace Chirp.Core.DTO;

public class CheepDTO
{
    public required string Text { get; set; }
    public required string Author { get; set; }
    public required string TimeStamp { get; set; }

    public bool IsImage => IsImageUrl(Text); // Detect if the text is an image link

    private static bool IsImageUrl(string text)
    {
        var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
        if (Uri.TryCreate(text, UriKind.Absolute, out var uri))
        {
            return validExtensions.Any(ext => uri.AbsolutePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }
        return false;
    }
}