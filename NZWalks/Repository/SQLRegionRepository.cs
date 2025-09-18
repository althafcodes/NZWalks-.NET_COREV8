using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid ID)
        {
            var existingregion = await dbContext.Regions.FirstOrDefaultAsync(r => r.ID == ID);

            if (existingregion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingregion);
            await dbContext.SaveChangesAsync();
            return (existingregion);
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetIDAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(id => id.Equals(id));
        }

        public async Task<Region?> UpdateAsync(Guid ID, Region region)
        {
            var existingregion = await dbContext.Regions.FirstOrDefaultAsync(r => r.ID == ID);


            if (existingregion == null)
            {
                return null;
            }

            existingregion.Code = region.Code;
            existingregion.RegionImageURL = region.RegionImageURL;
            existingregion.Name = region.Name;

            await dbContext.SaveChangesAsync();

            return existingregion;
        }
    }
}
