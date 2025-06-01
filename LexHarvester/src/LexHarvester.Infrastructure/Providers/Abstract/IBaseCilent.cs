namespace LexHarvester.Infrastructure.Providers;

public interface IBaseCilent<T,TRequest> where T : class where TRequest : class
{
    Task<T> SendAsync(TRequest request);
}
