using AutoMapper;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
             CreateMap<Region, RegionDTO>().ReverseMap();   
             CreateMap<AddRegionRequestDTO, Region>().ReverseMap();   
             CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();   
             CreateMap<AddWalksRequestDTO, Walk>().ReverseMap();   
             CreateMap<Walk, WalkDTO>().ReverseMap();   
             CreateMap<Difficulty, DifficultyDTO>().ReverseMap();   
             CreateMap<UpdateWalkRequestDTO, Walk>().ReverseMap();   
             CreateMap<ImageUploadRequestDTO, Image>().ReverseMap();   
        }
    }
}
