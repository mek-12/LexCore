using System;

namespace LexHarvester.Infrastructure.Providers.Request;

public class LexBaseRequest
{
    public string ApplicationName { get; set; } = "UyapMevzuat";
    public bool Paging { get; set; } = true;
}
