using System.Collections.Concurrent;
using AutoMapper;
using LexHarvester.Domain.Entities;
using Navend.Core.Data;
using Navend.Core.Step.Abstract;
using Navend.Core.UOW;

namespace LexHarvester.Application.Steps.CaseLawDocumentReferenceSync;

public class CaseLawDocumentReferenceUpdateStep(IUnitOfWork unitOfWork,
                                                CaseLawDocumentReferenceContext context) : IStep<CaseLawDocumentReferenceContext>
{
    public int Order => 2;
    private readonly IRepository<HarvestingState, int> _harvestingRepository = unitOfWork.GetRepository<HarvestingState, int>();
    private readonly IRepository<CaseLawDocumentReference, long> _documentReferenceRepository = unitOfWork.GetRepository<CaseLawDocumentReference, long>();
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        const int batchSize = 10000;
        int counter = 0;
        var buffer = new List<CaseLawDocumentReference>(batchSize);
        int total = context.CaseLawDocumentReferences.Count;
        int processed = 0;
        context.CaseLawDocumentReferences = new (
            context.CaseLawDocumentReferences
                .DistinctBy(x => x.DocumentId)
        );
        foreach (var item in context.CaseLawDocumentReferences)
        {
            buffer.Add(item);
            counter++;
            processed++;

            // Eğer batch tamamlandıysa ama bu son kayıtlar değilse
            if (counter == batchSize && processed < total)
            {
                await _documentReferenceRepository.AddRangeAsync(buffer, saveChanges: false);
                buffer.Clear();
                counter = 0;
            }
        }

        // 🔚 Kalan kayıtlar varsa, son batch için SaveChanges çalıştır
        if (buffer.Any())
        {
            await _documentReferenceRepository.AddRangeAsync(buffer, saveChanges: true);
        }
    }
}
