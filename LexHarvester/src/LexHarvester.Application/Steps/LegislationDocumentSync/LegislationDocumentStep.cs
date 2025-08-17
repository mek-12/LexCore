using LexHarvester.Application.Extensions;
using LexHarvester.Domain.Entities;
using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Request;
using Navend.Core.Data;
using Navend.Core.Step.Abstract;
using Navend.Core.UOW;

namespace LexHarvester.Application.Steps.LexDocumentSync;

public class LegislationDocumentStep(IUnitOfWork unitOfWork, ILegislationDocumentProvider documentProvider) : IStep<LegislationDocumentStepContext>
{
    public int Order => 0;
    private readonly IRepository<LegislationDocumentReference, long> legislationDocumentReference = unitOfWork.GetRepository<LegislationDocumentReference, long>();
    private readonly IRepository<LexDocument, long> lexDocumentRepository = unitOfWork.GetRepository<LexDocument, long>();

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        await unitOfWork.StartTransactionAsync();

        try
        {
            while (await legislationDocumentReference.GetRemainingDownloadCount() > 0)
            {
                await unitOfWork.StartTransactionAsync();
                var legislationDocuments = await legislationDocumentReference.SelectAsync(p => !p.Downloaded,
                                                                                          s => new { s.Id ,s.Downloaded, s.LegislationId },
                                                                                          false, 1000);
                // Process legislationDocuments in parallel with a max degree of parallelism of 20
                await Parallel.ForEachAsync(legislationDocuments, new ParallelOptions { MaxDegreeOfParallelism = 20, CancellationToken = cancellationToken }, async (doc, ct) =>
                {
                    // TODO: Replace this with your actual provider logic to get the response for each document
                    // Example:
                    var request = new LegislationDocumentRequest();
                    request.Data.DocumentId = doc.LegislationId;
                    var response = await documentProvider.SendAsync(request);
                    
                    Console.WriteLine($"Processing document Id: {doc.Id}");
                });
                await unitOfWork.CommitTransactionAsync();
            }
        }
        catch (System.Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

}
