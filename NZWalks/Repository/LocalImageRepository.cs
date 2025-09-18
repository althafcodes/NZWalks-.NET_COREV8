using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repository
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor, NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.contextAccessor = contextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, 
                "Images", image.FileName+ image.FileExtension);


            //copying file to local folder
            using(var stream = new FileStream(localPath, FileMode.Create))
            {
                await image.File.CopyToAsync(stream);
            }

            var urlFilePath = $"{contextAccessor.HttpContext.Request.Scheme}://{contextAccessor.HttpContext.Request.Host}{contextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
