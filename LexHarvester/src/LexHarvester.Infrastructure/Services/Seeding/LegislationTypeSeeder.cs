using LexHarvester.Domain.Entities;
using Navend.Core.Data;
using Navend.Core.UOW;

namespace LexHarvester.Infrastructure.Services.Seeding;
public class LegislationTypeSeeder : ITableSeeder
{
    private readonly IUnitOfWork _unitOfWork;
    private IRepository<LegislationType, int> _repository;
    public LegislationTypeSeeder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<LegislationType, int>();
    }

    public async Task SeedIfTableEmptyAsync(CancellationToken cancellationToken = default)
    {
        bool isEmpty = await _repository.GetCountAsync(r => r.Id == r.Id) != 0; // koşul doğru olmadı. Sonra yine bakalım
        if (!isEmpty)
            return;

        var types = new List<LegislationType>
        {
            new LegislationType { LegislationTypeId = 1, LegislationTypeCode = "KANUN", LegislationTypeTitle = "Kanun", OrderNumber = 1, Count = 0 },
            new LegislationType { LegislationTypeId = 2, LegislationTypeCode = "YÖNETMELİK", LegislationTypeTitle = "Yönetmelik", OrderNumber = 2, Count = 0 }
            // Diğer türler eklenebilir
        };

        await _repository.AddRangeAsync(types);
    }
}