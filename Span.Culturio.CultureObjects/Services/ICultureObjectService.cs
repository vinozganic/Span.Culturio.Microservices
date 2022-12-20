using System;
using Span.Culturio.Core.Models.CultureObject;

namespace Span.Culturio.CultureObjects.Services
{
    public interface ICultureObjectService
    {
        Task<IEnumerable<CultureObjectDto>> GetCultureObjectsAsync();
        Task<CultureObjectDto> GetCultureObjectAsync(int id);
        Task<CultureObjectDto> CreateCultureObjectAsync(CreateCultureObjectDto request);

    }
}

