using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repository
{
    public class SQLWalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalksRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Note:whenever Repository created need to inject on builder
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingwalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingwalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingwalk);
            await dbContext.SaveChangesAsync();
            return existingwalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            //before having filters
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //filter
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //sort
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKM) : walks.OrderByDescending(x => x.LengthInKM);
                }
            }


            //pagination
            //formula -> Skip X no. results and Take Y no. results
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIDAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, UpdateWalkRequestDTO updateWalkRequest)
        {
            var existingwalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if(existingwalk == null)
            {
                return null;
            }

            existingwalk.Name = updateWalkRequest.Name;
            existingwalk.Description = updateWalkRequest.Description;
            existingwalk.LengthInKM = updateWalkRequest.LengthInKM;
            existingwalk.WalkImageURL = updateWalkRequest.WalkImageURL;
            existingwalk.DifficultyID = updateWalkRequest.DifficultyID;
            existingwalk.RegionID = updateWalkRequest.RegionID;

            await dbContext.SaveChangesAsync();

            return existingwalk;
        }
    }
}
