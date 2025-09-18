using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to 3 characters")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
