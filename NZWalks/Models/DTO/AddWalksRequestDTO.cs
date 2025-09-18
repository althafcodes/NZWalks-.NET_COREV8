using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class AddWalksRequestDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Range(0,500)]
        public string Description { get; set; }
        [Required]
        public double LengthInKM { get; set; }
        public string? WalkImageURL { get; set; }
        [Required]
        public Guid DifficultyID { get; set; }
        [Required]
        public Guid RegionID { get; set; }
    }
}
