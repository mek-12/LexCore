using System;
using LexHarvester.Application.Harvesting.SubtypeProviders.Abstract;
using LexHarvester.Domain.Entities;
using Navend.Core.Data;
using Navend.Core.UOW;

namespace LexHarvester.Application.Harvesting.SubtypeProviders.Concrete;

public class LegislationSubtypeProvider : ISubtypeProvider
{
    private readonly IRepository<LegislationType,int> repository;

    public LegislationSubtypeProvider(IUnitOfWork unitOfWork) => repository = unitOfWork.GetRepository<LegislationType, int>();

    public async Task<List<string>> GetSubtypesAsync()
    {
        var subtypes = await repository.GetAllAsync();
        return subtypes.Select(x => x.LegislationTypeCode).ToList();
    }
}