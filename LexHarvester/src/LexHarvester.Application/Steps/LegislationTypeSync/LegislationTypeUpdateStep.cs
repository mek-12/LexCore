using LexHarvester.Domain.Entities;
using LexHarvester.Infrastructure.Providers;
using LexHarvester.Infrastructure.Providers.Request;
using Navend.Core.Data;
using Navend.Core.Step.Abstract;
using Navend.Core.UOW;

namespace LexHarvester.Application.Steps.LegislationTypeSync;

public class LegislationTypeUpdateStep(IUnitOfWork unitOfWork,LegislationTypeStepContext context , ILegislationTypeProvider legislationTypeProvider) : IStep<LegislationTypeStepContext>
{
    public int Order => 0;
    private IRepository<LegislationType, int> _repository = unitOfWork.GetRepository<LegislationType, int>();
    private ILegislationTypeProvider _legislationTypeProvider = legislationTypeProvider;
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        // Servisten veri al
        var legislationTypeResponse = await _legislationTypeProvider.SendAsync(new LegislationTypeRequest());
        if (legislationTypeResponse == null || legislationTypeResponse.Data == null || !legislationTypeResponse.Data.Any())
            return;

        var incomingTypes = legislationTypeResponse.Data;
        var existingTypes = await _repository.GetAllAsync(); // tüm verileri al

        var typesToUpdate = new List<LegislationType>();
        var typesToInsert = new List<LegislationType>();

        foreach (var incoming in incomingTypes)
        {
            var existing = existingTypes.FirstOrDefault(x => x.LegislationTypeCode == incoming.LegislationTypeCode);
            if (existing != null)
            {
                // Eğer sadece count farklıysa güncelle
                if (existing.Count != incoming.Count)
                {
                    existing.Count = incoming.Count;
                    existing.LastOperationDate = incoming.LastOperationDate;
                    typesToUpdate.Add(existing);
                }
            }
            else
            {
                // Yeni kayıt
                typesToInsert.Add(new LegislationType
                {
                    LegislationTypeId = incoming.LegislationTypeId,
                    LegislationTypeCode = incoming.LegislationTypeCode,
                    LegislationTypeTitle = incoming.LegislationTypeTitle,
                    OrderNumber = incoming.OrderNumber,
                    LastOperationDate = incoming.LastOperationDate,
                    Count = incoming.Count
                });
            }
        }

        if (typesToUpdate.Any())
        {
            await _repository.UpdateRangeAsync(typesToUpdate);
        }

        if (typesToInsert.Any())
            await _repository.AddRangeAsync(typesToInsert);
        var legislationTypes = await _repository.GetAllAsync();
        context.LegislationTypes.AddRange(legislationTypes);
    }
}
