using LexHarvester.Domain.DTOs;

namespace LexHarvester.Domain.Entities;

public class RequestEndpointConfig
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Method { get; set; }
    public string Description { get; set; }
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