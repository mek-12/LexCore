using LexHarvester.Domain.Entities;
using LexHarvester.Infrastructure.Providers;
using LexHarvester.Infrastructure.Providers.Request;
using Navend.Core.Data;
using Navend.Core.UOW;

namespace LexHarvester.Application.Seeding.Concrete;

public class CaseLawTypeSync : ITableSync
{
    public int Order => 1;
    private readonly IUnitOfWork _unitOfWork;
    private IRepository<CaseLawType, int> _repository;
    private ICaseLawTypeProvider _caseLawTypeProvider;
    public CaseLawTypeSync(IUnitOfWork unitOfWork, ICaseLawTypeProvider caseLawTypeProvider)
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<CaseLawType, int>();
        _caseLawTypeProvider = caseLawTypeProvider;
    }
    
    public async Task SyncAsync(CancellationToken cancellationToken = default)
    {
        bool isEmpty = await _repository.GetCountAsync(r => r.Id == r.Id) == 0; // koşul doğru olmadı. Sonra yine bakalım
        if (!isEmpty)
            return;
        var caseLawTypeResponse =  await _caseLawTypeProvider.SendAsync(new CaseLawTypeRequest());
        if (caseLawTypeResponse == null)
            return;
        var types = new List<CaseLawType>();
        foreach (var type in caseLawTypeResponse.Data)
        {
            types.Add(new CaseLawType
            {
                Name = type.Name,
                Description = type.Description,
                Number = type.Number,
                Count = type.Count
            });
        }
        if (types.Count == 0)
            throw new InvalidOperationException("No CaseLaw Types found in the response.");
        await _repository.AddRangeAsync(types);
    }
}
