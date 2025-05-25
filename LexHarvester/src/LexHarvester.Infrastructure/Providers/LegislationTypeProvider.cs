using LexHarvester.Domain.DTOs;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;
using Newtonsoft.Json;

namespace LexHarvester.Infrastructure.Providers;

public class LegislationTypeProvider : ILegislationTypeProvider
{
    private readonly HttpClient _httpClient;
    public LegislationTypeProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<LegislationTypeResponse> GetAsync(LegislationTypeRequest legislationTypeRequest)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "mevzuat/mevzuatTypes")
            {
                Content = new StringContent(JsonConvert.SerializeObject(legislationTypeRequest), System.Text.Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(request, CancellationToken.None);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(CancellationToken.None);
            var result = JsonConvert.DeserializeObject<LegislationTypeResponse>(content);
            return result ?? throw new InvalidOperationException("Failed to deserialize LegislationTypeDto from response content.");
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}
