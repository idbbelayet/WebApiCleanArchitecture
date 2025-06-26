namespace Application.DTOs
{
    public class RegionDto
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public DateTime DataCollectionDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
