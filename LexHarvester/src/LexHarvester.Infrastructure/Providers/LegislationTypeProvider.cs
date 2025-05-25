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
        var request = new HttpRequestMessage(HttpMethod.Post, "emsal-karar/getBirimler")
        {
            Content = new StringContent(JsonConvert.SerializeObject(legislationTypeRequest), System.Text.Encoding.UTF8, "application/json")
        };
        var response = await _httpClient.SendAsync(request, CancellationToken.None);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(CancellationToken.None);
        return JsonConvert.DeserializeObject<LegislationTypeResponse>(content)
            ?? throw new InvalidOperationException("Failed to deserialize LegislationTypeDto from response content.");
    }
}
