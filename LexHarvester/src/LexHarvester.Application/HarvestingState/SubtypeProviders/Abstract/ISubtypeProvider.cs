using System;

namespace LexHarvester.Application.Harvesting.SubtypeProviders.Abstract;

public interface ISubtypeProvider
{
    Task<List<string>> GetSubtypesAsync();
}
