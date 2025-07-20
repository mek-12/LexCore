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
    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        List<Task> tasks = new();
        tasks.Add(_harvestingRepository.UpdateRangeAsync(context.HarvestingStates));
        if (context.CaseLawDocumentReferences.Any())
            tasks.Add(_documentReferenceRepository.AddRangeAsync(context.CaseLawDocumentReferences));

        return Task.WhenAll(tasks);
    }
}
