using System.Text;
using LexHarvester.Infrastructure.Providers.Request;
using Newtonsoft.Json;

namespace LexHarvester.Infrastructure.Providers.Concrete;

public abstract class BaseHttpProvider<TRequest, TResponse> : IBaseCilent<TRequest, TResponse>
    where TRequest : LexBaseRequest
    where TResponse : class
{
    private readonly HttpClient _httpClient;

    protected BaseHttpProvider(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    protected abstract HttpMethod HttpMethod { get; }
    protected virtual string ContentType { get; } = "application/json";
    public async Task<TResponse> SendAsync(TRequest requestModel)
    {
        try
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod,
                Content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, ContentType)
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(content);

            if (result == null)
            {
                throw new InvalidOperationException($"[{GetType().Name}] response deserialization failed.");
            }

            Console.WriteLine($"[{GetType().Name}] Request succeeded.");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{GetType().Name}] Request failed: {ex.Message}");
            throw;
        }
    }
}
