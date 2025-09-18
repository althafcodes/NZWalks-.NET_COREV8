using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.CustomActionFilters;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repository;
using System.Text.Json;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper,
            ILogger<RegionsController> logger 
            )
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //throw new Exception("test error");
                var regions = await regionRepository.GetAllAsync();

                //List<RegionDTO> regionsDTO = new List<RegionDTO>();
                //foreach (var region in regions)
                //{
                //    regionsDTO.Add(new RegionDTO()
                //    {
                //        ID = region.ID,
                //        Code = region.Code,
                //        Name = region.Name,
                //        RegionImageURL = region.RegionImageURL
                //    });
                //}

                var regionsDTO = mapper.Map<List<RegionDTO>>(regions);


                return Ok(regionsDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var region = await regionRepository.GetIDAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            //RegionDTO regionsDTO = new RegionDTO()
            //{
            //    ID = region.ID,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageURL = region.RegionImageURL
            //};

            var regionsDTO = mapper.Map<RegionDTO>(region);
            return Ok(regionsDTO);
        }

        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRequestDTO)
        {
            //var regionDomainModel = new Region()
            //{
            //    Code = addRequestDTO.Code,
            //    Name = addRequestDTO.Name,
            //    RegionImageURL = addRequestDTO.RegionImageURL
            //};
            
            var regionDomainModel = mapper.Map<Region>(addRequestDTO);

            await regionRepository.CreateAsync(regionDomainModel);

            //var regionDto = new RegionDTO
            //{
            //    ID = regionDomainModel.ID,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageURL = regionDomainModel.RegionImageURL
            //};

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetByID), new { id = regionDomainModel.ID }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequest)
        {
            //Region regiondomain = new Region
            //{
            //    Code = updateRegionRequest.Code,
            //    Name = updateRegionRequest.Name,
            //    RegionImageURL = updateRegionRequest.RegionImageURL
            //};

            
            var regiondomain = mapper.Map<Region>(updateRegionRequest);

            var regionDomainModel = await regionRepository.UpdateAsync(id, regiondomain);

            if (regionDomainModel == null)
            {
                return null;
            }


            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedRegion = await regionRepository.DeleteAsync(id);

            if (deletedRegion == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDTO>(deletedRegion);

            return Ok(regionDto);
        }
    }
}
