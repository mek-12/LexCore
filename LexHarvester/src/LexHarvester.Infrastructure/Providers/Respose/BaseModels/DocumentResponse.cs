namespace LexHarvester.Infrastructure.Providers.Respose.BaseModels;

public class DocumentResponse
{
    public DocumentData Data { get; set; }
    public ResponseMetadata Metadata { get; set; } = new();
}

public class DocumentData
{
    public string Content { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public int Version { get; set; } = default;
}
