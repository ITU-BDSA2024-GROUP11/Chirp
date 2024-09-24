using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
[Route("")]
[ApiController]

public class CheepsController : ControllerBase
{
    
    private const string FilePath = "cheeps.csv";
    
    [HttpPost("cheep")]
    public IActionResult PostCheep([FromBody] Cheep cheep)
    {
        
       using (var writer = new StreamWriter(FilePath, true))
        {
            writer.WriteLine($"{cheep.Author},{cheep.Message},{cheep.Timestamp}");
        }
       return Ok();
    }
    
    [HttpGet("cheeps")]
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

public record Cheep(string Author, string Message, long Timestamp);