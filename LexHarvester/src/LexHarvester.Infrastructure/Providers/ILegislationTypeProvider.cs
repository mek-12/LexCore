using System;
using LexHarvester.Domain.DTOs;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;

namespace LexHarvester.Infrastructure.Providers;

public interface ILegislationTypeProvider : IBaseCilent<LegislationTypeResponse, LegislationTypeRequest>
{

}
