using LexHarvester.Application.Harvesting.SubtypeProviders.Abstract;
using LexHarvester.Domain.Entities;
using Navend.Core.Data;
using Navend.Core.UOW;

namespace LexHarvester.Application.Harvesting.SubtypeProviders.Concrete;

public class CaseLawSubtypeProvider : ISubtypeProvider
{
    private readonly IRepository<CaseLawType,int> _repository;

    public CaseLawSubtypeProvider(IUnitOfWork unitOfWork) => _repository = unitOfWork.GetRepository<CaseLawType, int>();

    public async Task<List<string>> GetSubtypesAsync()
    {
        var caseLawTypes = await _repository.GetAllAsync();
        return caseLawTypes.Select(x => x.Name).ToList();
    }
}
