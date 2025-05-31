using LexHarvester.Domain.DTOs;
using Navend.Core.Data;

namespace LexHarvester.Domain.Entities;

public class RequestEndpointConfig:IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}


public static class Extension {
    public static RequestEndpointConfigDto AsDTO(this RequestEndpointConfig requestEndpointConfig) =>
                                                 new RequestEndpointConfigDto{
                                                    Description = requestEndpointConfig.Description,
                                                    Method = requestEndpointConfig.Method,
                                                    Name= requestEndpointConfig.Name,
                                                    Url = requestEndpointConfig.Url
                                                 };
}