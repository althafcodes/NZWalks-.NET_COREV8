using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Data
{
    public class NZWalksAuthDBContext : IdentityDbContext
    {
        public NZWalksAuthDBContext(DbContextOptions<NZWalksAuthDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleID = "7f3a4f7c-6b21-4a8c-91a6-0e58d9a4b8de";
            var writerRoleID = "c2b1d81e-9d54-44b2-9351-5c9d6b83f4e7";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = readerRoleID,
                    ConcurrencyStamp = readerRoleID,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleID,
                    ConcurrencyStamp = writerRoleID,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
