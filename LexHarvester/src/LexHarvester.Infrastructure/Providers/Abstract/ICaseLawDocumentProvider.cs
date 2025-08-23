using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;

namespace LexHarvester.Infrastructure.Providers.Abstract;

public interface ICaseLawDocumentProvider: IBaseCilent<CaseLawDocumentRequest, CaseLawDocumentResponse> { }