using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class CheepsController : ControllerBase
{
    private const string FilePath = "cheeps.csv";

    [HttpPost("cheep")]
    public IActionResult PostCheep([FromBody] String message)
    {

        var cheep = new Cheep
        (
            Environment.UserName, message, DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        );
        
        // Append to CSV
        using (var writer = new StreamWriter(FilePath, true))
        {
            writer.WriteLine($"{cheep.Author},{cheep.Message},{cheep.Timestamp}");
        }

        return CreatedAtAction(nameof(GetCheeps), new { author = cheep.Author, timestamp = cheep.Timestamp }, cheep);
    }

    [HttpGet("read")]
    public IActionResult GetCheeps()
    {
        if (!System.IO.File.Exists(FilePath))
        {
            return Ok(new List<Cheep>());
        }

        var cheeps = System.IO.File.ReadAllLines(FilePath)
            .Select(line => line.Split(','))
            .Select(parts => new Cheep(parts[0], parts[1], long.Parse(parts[2])))
            .ToList();

        return Ok(cheeps);
    }
}
