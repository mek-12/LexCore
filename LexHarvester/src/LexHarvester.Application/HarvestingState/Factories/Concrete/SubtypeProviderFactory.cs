using LexHarvester.Application.Harvesting.SubtypeProviders.Abstract;
using LexHarvester.Application.Harvesting.SubtypeProviders.Concrete;
using LexHarvester.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace LexHarvester.Application.Harvesting.Factories.Concrete;

public class SubtypeProviderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public SubtypeProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISubtypeProvider GetProvider(DocumentType docType)
    {
        return docType switch
        {
            DocumentType.Legislation => _serviceProvider.GetRequiredService<LegislationSubtypeProvider>(),
            DocumentType.CaseLaw => _serviceProvider.GetRequiredService<CaseLawSubtypeProvider>(),
            _ => throw new NotSupportedException($"{docType} is not supported")
        };
    }
}
