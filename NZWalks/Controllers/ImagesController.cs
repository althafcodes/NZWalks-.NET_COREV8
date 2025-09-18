using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repository;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public ImagesController(IMapper mapper, IImageRepository imageRepository)
        {
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        //post -> /api/Images/upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO requestDTO)
        {
            ValidateFileUpload(requestDTO);

            if (ModelState.IsValid)
            {
                //dto to domain
                var imageDomain = mapper.Map<Image>(requestDTO);
                imageDomain.FileExtension = Path.GetExtension(requestDTO.File.FileName);
                imageDomain.FileSizeInBytes = requestDTO.File.Length;


                //use repository to upload image
                await imageRepository.Upload(imageDomain);

                return Ok(imageDomain);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO requestDTO)
        {
            var allowedExtns = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtns.Contains(Path.GetExtension(requestDTO.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported file format");
            }

            if(requestDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "Invalid File size");
            }
        }
    }
}
