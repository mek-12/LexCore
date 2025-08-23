using Navend.Core.UOW;
using Navend.Core.Step.Abstract;
using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Domain.Entities;
using Navend.Core.Data;
using LexHarvester.Application.Extensions;
using LexHarvester.Infrastructure.Providers.Respose;
using System.Collections.Concurrent;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Helper.Utils.Converting;
using LexHarvester.Domain.Enums;

namespace LexHarvester.Application.Steps.CaseLawDocumentSync;

public class CaseLawDocumentStep(IUnitOfWork unitOfWork,
                                ICaseLawDocumentProvider documentProvider) : IStep<CaseLawDocumentSyncContext>
{
    public int Order => 0;
    private readonly IRepository<CaseLawDocumentReference, long> caseLawDocumentReferenceRepository = unitOfWork.GetRepository<CaseLawDocumentReference, long>();
    private readonly IRepository<LexDocument, long> lexDocumentRepository = unitOfWork.GetRepository<LexDocument, long>();
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            while (await caseLawDocumentReferenceRepository.GetRemainingDownloadCount() > 0)
            {
                await unitOfWork.StartTransactionAsync();
                var documentReferences = await caseLawDocumentReferenceRepository.SelectAsync(p => !p.Downloaded,
                                                                                          s => new { s.Id, s.Downloaded, s.DocumentId },
                                                                                          false, 1000);
                ConcurrentDictionary<string, CaseLawDocumentResponse> documentResponses = new ConcurrentDictionary<string, CaseLawDocumentResponse>();
                // Process legislationDocuments in parallel with a max degree of parallelism of 20
                await Parallel.ForEachAsync(documentReferences, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount, CancellationToken = cancellationToken }, async (doc, ct) =>
                {
                    // TODO: Replace this with your actual provider logic to get the response for each document
                    // Example:
                    var request = new CaseLawDocumentRequest();
                    request.Data.DocumentId = doc.DocumentId;
                    var response = await documentProvider.SendAsync(request);
                    _ = documentResponses.TryAdd(doc.DocumentId, response);
                    Console.WriteLine($"Processing document Id: {doc.Id}");
                });

                foreach (var item in documentResponses)
                {
                    var documentId = item.Key;
                    var response = item.Value;
                    LexDocument lexDocument = new LexDocument
                    {
                        Content = response?.Data?.Content?.Base64ToBytes(),
                        DocumentType = DocumentType.CaseLaw,
                        DownloadedAt = DateTime.Now,
                        MimeType = response?.Data?.MimeType,
                        ReferenceId = documentId
                    };
                    await lexDocumentRepository.AddAsync(lexDocument);
                    await caseLawDocumentReferenceRepository.UpdatePropertiesAsync(
                        new CaseLawDocumentReference
                        {
                            Id = documentReferences.Where(d => d.DocumentId == documentId).First().Id,
                            Downloaded = true
                        },
                        l => l.Downloaded
                    );
                }
                await unitOfWork.CommitTransactionAsync();
                documentResponses.Clear();
            }
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
