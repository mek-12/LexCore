using LexHarvester.Domain.Entities;
using LexHarvester.Persistence.UOW;

namespace LexHarvester.Infrastructure.Services.Seeding;
public class LegislationTypeSeeder : ITableSeeder
{
    private readonly IUnitOfWork<LegislationType> _unitOfWork;

    public LegislationTypeSeeder(IUnitOfWork<LegislationType> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SeedIfTableEmptyAsync(CancellationToken cancellationToken = default)
    {

        bool isEmpty = !(await _unitOfWork.GetAllAsync()).Any();
        if (!isEmpty)
            return;

        var types = new List<LegislationType>
        {
            new LegislationType { LegislationTypeId = 1, LegislationTypeCode = "KANUN", LegislationTypeTitle = "Kanun", OrderNumber = 1, Count = 0 },
            new LegislationType { LegislationTypeId = 2, LegislationTypeCode = "YÖNETMELİK", LegislationTypeTitle = "Yönetmelik", OrderNumber = 2, Count = 0 }
            // Diğer türler eklenebilir
        };

        await _unitOfWork.AddRangeAsync(types);
    }
}