namespace LexHarvester.Domain.Entities;

public class RequestEndpointConfig
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Method { get; set; }
    public string Description { get; set; }
}
