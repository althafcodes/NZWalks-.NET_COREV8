using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repository
{
    public interface IWalksRepository
    {
        //Creating definition and interface
        //Note: always takes and returns domain model 
        ////whenever Repository created need to inject on builder
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null,bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIDAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, UpdateWalkRequestDTO updateWalkRequest);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
