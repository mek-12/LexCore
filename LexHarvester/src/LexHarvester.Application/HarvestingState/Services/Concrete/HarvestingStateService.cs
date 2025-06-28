using LexHarvester.Domain.Enums;
using LexHarvester.Domain.Entities;
using Navend.Core.UOW;
using LexHarvester.Application.Harvesting.Factories.Concrete;
using Navend.Core.Data;
namespace LexHarvester.Application.Harvesting.Services.Concrete;

public class HarvestingStateService
{
    private readonly IRepository<HarvestingState,int> _repository;
    private readonly SubtypeProviderFactory _providerFactory;
    private readonly IUnitOfWork _unitOfWork;
    public HarvestingStateService(IUnitOfWork unitOfWork, SubtypeProviderFactory providerFactory)
    {
        _repository = unitOfWork.GetRepository<HarvestingState, int>();
        _providerFactory = providerFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task EnsureStatesExistAsync(DocumentType docType)
    {
        var provider = _providerFactory.GetProvider(docType);
        var subtypes = await provider.GetSubtypesAsync();

        foreach (var subtype in subtypes)
        {
            var count = await _repository.GetCountAsync(x => x.DocumentType == docType && x.SubType == subtype);

            if (count <= 0)
            {
                await _repository.AddAsync(new HarvestingState
                {
                    DocumentType = docType,
                    SubType = subtype,
                    CurrentPage = 1,
                    Count = 0,
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }
        await _unitOfWork.CommitTransactionAsync();
    }
}

