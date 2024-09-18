using System;
using Chirp.CLI;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/Cheeps", () => "");

app.Run();
