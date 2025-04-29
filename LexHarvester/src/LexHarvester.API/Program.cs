using LexHarvester.Persistence;
using Microsoft.EntityFrameworkCore;
using LexHarvester.Application.Extensions;
using LexHarvester.API.Extensions;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Configure Services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Eğer ileride servisler eklersen burada builder.Services.AddScoped<>, AddSingleton<> gibi eklemeler yapılacak


builder.Services.AddInfrastructure(collection => {
    collection.AddEndpointsApiExplorer();
    collection.AddSwaggerGen();
    collection.AddControllers();
    collection.AddHf(builder.Configuration);
});

var app = builder.Build();
app.MapControllers();
app.UseHangfireDashboard("/hangfire");
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
    context.Database.Migrate();
}
app.UseHttpsRedirection();

// Sağlık kontrolü veya boş test için basit bir endpoint
app.MapGet("/", () => "LexHarvester API is running...");

app.Run();