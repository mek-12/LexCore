using System.Collections.Concurrent;
using System.Text.Json;
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
        context.CaseLawDocumentReferences = new (
            context.CaseLawDocumentReferences
                .DistinctBy(x => x.DocumentId)
        );
        SaveToJsonFile(context.HarvestingStates);
        await _documentReferenceRepository.BulkInsertAsync(context.CaseLawDocumentReferences, 10000);
        await _harvestingRepository.BulkInsertAsync(context.HarvestingStates, 10000, false);
    }

    public static void SaveToJsonFile(ConcurrentBag<HarvestingState> states)
    {
        // Uygulamanın çalıştığı dizinde dosya adı
        var fileName = Path.Combine(AppContext.BaseDirectory, "harvesting_states.json");

        // Snapshot al (thread-safe)
        var snapshot = states.ToList();

        // JSON opsiyonları (güzel görünsün diye)
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        // Serialize et ve dosyaya yaz
        var json = JsonSerializer.Serialize(snapshot, options);
        File.WriteAllText(fileName, json);
    }
}
