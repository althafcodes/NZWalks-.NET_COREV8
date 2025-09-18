using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
                
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions  { get; set; }
        public DbSet<Walk> Walks  { get; set; }
        public DbSet<Image> Images { get; set; }

        //On Model creating if the below models does not exist ef core checks and create below data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seeding difficulty data 
            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("3acb3af2-a6ee-4c29-8a0a-b481ee03a574"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("8fb0a33c-ae5d-44fa-80a2-564e319b672c"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("9ff5d3d5-9d2a-47a3-8c51-abc5034f3121"),
                    Name = "Hard"
                }

            };
            //seed data into the difficulty data table
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            var regions = new List<Region>
            {
                new Region
                {
                    ID = Guid.Parse("3f5e2c2b-7f8a-4f1e-9b5f-91c2b184e001"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageURL = "https://example.com/images/auckland.png"
                },
                new Region
                {
                    ID = Guid.Parse("7c8b9a0d-2e3f-4c6a-b7d9-23a7b681e002"),
                    Name = "Wellington",
                    Code = "WLG",
                    RegionImageURL = "https://example.com/images/wellington.png"
                },
                new Region
                {
                    ID = Guid.Parse("9a1c2d3e-4f5a-678b-9c0d-1e2f3a4b5003"),
                    Name = "Canterbury",
                    Code = "CAN",
                    RegionImageURL = "https://example.com/images/canterbury.png"
                },
                new Region
                {
                    ID = Guid.Parse("0a9b8c7d-6e5f-4d3c-b2a1-9f8e7d6c4004"),
                    Name = "Waikato",
                    Code = "WKO",
                    RegionImageURL = "https://example.com/images/waikato.png"
                },
                new Region
                {
                    ID = Guid.Parse("12345678-90ab-cdef-1234-567890abcdef"),
                    Name = "Otago",
                    Code = "OTA",
                    RegionImageURL = "https://example.com/images/otago.png"
                },
                new Region
                {
                    ID = Guid.Parse("abcdef12-3456-7890-abcd-ef1234567890"),
                    Name = "Bay of Plenty",
                    Code = "BOP",
                    RegionImageURL = "https://example.com/images/bayofplenty.png"
                }
            };

            //seed data into the regions data table
            modelBuilder.Entity<Region>().HasData(regions);



        }
    }
}
