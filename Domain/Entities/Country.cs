using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Country
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }

        public int RegionId { get; set; }
        public string CountryName { get; set; }

        // Navigation property
        public Region Region { get; set; }
    }
}
