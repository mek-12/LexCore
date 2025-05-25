using LexHarvester.Domain.Entities;
using LexHarvester.Infrastructure.Providers;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;
using Navend.Core.Data;
using Navend.Core.UOW;

namespace LexHarvester.Application.Services.Seeding;
public class LegislationTypeSync : ITableSync
{
    private readonly IUnitOfWork _unitOfWork;
    private IRepository<LegislationType, int> _repository;
    private Lazy<ILegislationTypeProvider> _legislationTypeProvider;
    public LegislationTypeSync(IUnitOfWork unitOfWork, Lazy<ILegislationTypeProvider> legislationTypeProvider)
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<LegislationType, int>();
        _legislationTypeProvider = legislationTypeProvider;
    }
    
    public async Task SyncAsync(CancellationToken cancellationToken = default)
    {
        bool isEmpty = await _repository.GetCountAsync(r => r.Id == r.Id) != 0; // koşul doğru olmadı. Sonra yine bakalım
        if (!isEmpty)
            return;
        LegislationTypeResponse legislationTypeResponse =  await _legislationTypeProvider.Value.GetAsync(new LegislationTypeRequest());
        if (legislationTypeResponse == null)
            return;
        var types = new List<LegislationType>();
        foreach (var type in legislationTypeResponse.Data)
        {
            types.Add(new LegislationType
            {
                LegislationTypeId = type.LegislationTypeId,
                LegislationTypeCode = type.LegislationTypeCode,
                LegislationTypeTitle = type.LegislationTypeTitle,
                OrderNumber = type.OrderNumber,
                LastOperationDate = type.LastOperationDate,
                Count = type.Count
            });
        }
        if (types.Count == 0)
            throw new InvalidOperationException("No Legislation Types found in the response.");
        await _repository.AddRangeAsync(types);
    }
}