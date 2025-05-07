using LexHarvester.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace LexHarvester.Application.Utils;
/// <summary>
/// TransactionRunner created for hangfire or other backgroun jobs mechanisms.
/// Jobs must be run with TransactioRunner for UOW approach. 
/// </summary>
public static class TransactionRunner {
    public static async Task RunAsync(IServiceProvider provider, Func<IServiceProvider, Task> action) {
        using var scope = provider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try { 
            await action(scope.ServiceProvider);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        } catch {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
