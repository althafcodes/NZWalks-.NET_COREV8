namespace NZWalks.Models.Domain
{
    public class Region
    {
        public Guid ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageURL{ get; set; }
    }
}
