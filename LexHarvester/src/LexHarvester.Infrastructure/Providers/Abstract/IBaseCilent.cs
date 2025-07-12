using LexHarvester.Infrastructure.Providers.Request;

namespace LexHarvester.Infrastructure.Providers;

public interface IBaseCilent<TRequest, T> where T : class where TRequest : LexBaseRequest
{
    Task<T> SendAsync(TRequest request);
}
