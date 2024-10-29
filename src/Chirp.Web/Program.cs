using Chirp.Core.CheepServiceInterface;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure;
using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

// Delete the chirp.db file if it exists
// Construct the correct path to the chirp.db file
var dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "chirp.db");
Console.WriteLine("HERE: " + dbFilePath);
if (File.Exists(dbFilePath))
{
    Console.WriteLine("Deleting existing database file chirp.db");
    File.Delete(dbFilePath);
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
        options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ChirpDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Migrate Database and Seed
using (var scope = app.Services.CreateScope())
{
    using var context = scope.ServiceProvider.GetRequiredService<ChirpDBContext>();
    context.Database.Migrate();
    DbInitializer.SeedDatabase(context);
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();

namespace Chirp.Razor
{
}