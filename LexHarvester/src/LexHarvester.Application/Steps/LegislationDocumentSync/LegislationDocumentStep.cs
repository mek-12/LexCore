using System.Collections.Concurrent;
using LexHarvester.Application.Extensions;
using LexHarvester.Domain.Entities;
using LexHarvester.Domain.Enums;
using LexHarvester.Helper.Utils.Converting;
using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;
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
        try
        {
            while (await legislationDocumentReference.GetRemainingDownloadCount() > 0)
            {
                await unitOfWork.StartTransactionAsync();
                var documentReferences = await legislationDocumentReference.SelectAsync(p => !p.Downloaded,
                                                                                          s => new { s.Id, s.Downloaded, s.LegislationId },
                                                                                          false, 1000);
                ConcurrentDictionary<string, LegislationDocumentResponse> legislationDocumentResponses = new ConcurrentDictionary<string, LegislationDocumentResponse>();
                // Process legislationDocuments in parallel with a max degree of parallelism of 20
                await Parallel.ForEachAsync(documentReferences, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount, CancellationToken = cancellationToken }, async (doc, ct) =>
                {
                    // TODO: Replace this with your actual provider logic to get the response for each document
                    // Example:
                    var request = new LegislationDocumentRequest();
                    request.Data.Id = doc.LegislationId;
                    LegislationDocumentResponse response = await documentProvider.SendAsync(request);
                    _ = legislationDocumentResponses.TryAdd(doc.LegislationId, response);
                    Console.WriteLine($"Processing document Id: {doc.Id}");
                });

                foreach (var item in legislationDocumentResponses)
                {
                    var legislationId = item.Key;
                    var response = item.Value;
                    LexDocument lexDocument = new LexDocument
                    {
                        Content = response?.Data?.Content?.Base64ToBytes(),
                        DocumentType = DocumentType.Legislation,
                        DownloadedAt = DateTime.Now,
                        MimeType = response?.Data?.MimeType,
                        ReferenceId = legislationId
                    };
                    await lexDocumentRepository.AddAsync(lexDocument);
                    await legislationDocumentReference.UpdatePropertiesAsync(
                        new LegislationDocumentReference
                        {
                            Id = documentReferences.Where(d => d.LegislationId == legislationId).First().Id,
                            Downloaded = true
                        },
                        l => l.Downloaded
                    );
                }
                await unitOfWork.CommitTransactionAsync();
                legislationDocumentResponses.Clear();
            }
        }
        catch (System.Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
