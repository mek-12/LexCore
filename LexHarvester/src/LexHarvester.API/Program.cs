using LexHarvester.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Eğer ileride servisler eklersen burada builder.Services.AddScoped<>, AddSingleton<> gibi eklemeler yapılacak

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}
app.UseHttpsRedirection();

// Sağlık kontrolü veya boş test için basit bir endpoint
app.MapGet("/", () => "LexHarvester API is running...");

app.Run();
