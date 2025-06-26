using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Region
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public DateTime DataCollectionDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public ICollection<Country> Countries { get; set; } = new List<Country>();
    }
}
