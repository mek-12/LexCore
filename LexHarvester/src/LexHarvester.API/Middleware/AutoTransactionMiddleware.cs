using LexHarvester.Persistence;

namespace LexHarvester.API.Middleware;

public class AutoTransactionMiddleware {
    private readonly RequestDelegate _next;

    public AutoTransactionMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext) {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try {
            await _next(context);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        } catch {
            await transaction.RollbackAsync();
            throw;
        }
    }
}