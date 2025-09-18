using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetIDAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid ID, Region region);
        Task<Region?> DeleteAsync(Guid ID);
    }
}
