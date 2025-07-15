using LexHarvester.Persistence;
using Microsoft.EntityFrameworkCore;
using LexHarvester.Application.Extensions;
using LexHarvester.API.Extensions;
using Hangfire;
using LexHarvester.API.Middleware;
using Navend.Core.Extensions;

Console.WriteLine("Starting LexHarvester API...");
var builder = WebApplication.CreateBuilder(args);

// Configure Services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Eğer ileride servisler eklersen burada builder.Services.AddScoped<>, AddSingleton<> gibi eklemeler yapılacak

builder.Services.RegisterServices(collection => {
    collection.AddEfCoreUnitOfWork<ApplicationDbContext>(builder.Configuration);
    collection.AddEndpointsApiExplorer();
    collection.AddSwaggerGen();
    collection.AddControllers();
    collection.AddHf(connectionString);
});
WebApplication app;
try
{
    app = builder.Build();
}
catch (AggregateException ex)
{
    
    throw ex;
}

app.MapControllers();
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new AllowAllDashboardAuthorizationFilter() },
    IgnoreAntiforgeryToken = true
});
// Configure Middleware
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();   
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}
app.UseHttpsRedirection();
app.UseMiddleware<AutoTransactionMiddleware>();
// Sağlık kontrolü veya boş test için basit bir endpoint
app.MapGet("/", () => "LexHarvester API is running...");

app.Run();