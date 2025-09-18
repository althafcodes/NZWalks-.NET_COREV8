using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.CustomActionFilters;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repository;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalksRepository walksRepository;

        public WalksController(IMapper mapper, IWalksRepository walksRepository)
        {
            this.mapper = mapper;
            this.walksRepository = walksRepository;
        }

        //post -> api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDTO addWalksRequest)
        {

            //Map DTO(AddWalksRequestDTO) -> Domain Model(Walk)
            //Note : DTO and Domain model both should have same names if not need to manage on Automapper
            var walkdomainmodel = mapper.Map<Walk>(addWalksRequest);

            await walksRepository.CreateAsync(walkdomainmodel);

            //mapping Domain model -> DTO 
            return Ok(mapper.Map<WalkDTO>(walkdomainmodel));


        }

        //GET -> api/Walks?filerOn=name&filerQuery=track&sortBy=length&isAscending=false
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filerOn, [FromQuery] string? filerQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksList = await walksRepository.GetAllAsync(filerOn, filerQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //throw new Exception("test global exception");
            //DTO -> domain

            return Ok(mapper.Map<List<WalkDTO>>(walksList));
        }

        //GET -> api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var walkdomain = await walksRepository.GetByIDAsync(id);

            if (walkdomain == null)
            {
                return NotFound();
            }

            //domain to dto
            return Ok(mapper.Map<WalkDTO>(walkdomain));
        }

        //PUT -> api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDTO requestDTO)
        {
            
            //dto -> domain model
            var updatedomain = mapper.Map<Walk>(requestDTO);

            updatedomain = await walksRepository.UpdateAsync(id, requestDTO);

            if(updatedomain == null) return NotFound();

            return Ok(mapper.Map<WalkDTO>(updatedomain));
        }


        //Delete -> api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) { 
            var deletedWalk = await walksRepository.DeleteAsync(id);

            if (deletedWalk == null) return NotFound();

            return Ok(mapper.Map<WalkDTO>(deletedWalk));
        }
    }
}
