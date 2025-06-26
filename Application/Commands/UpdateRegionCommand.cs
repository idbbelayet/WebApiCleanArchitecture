using Application.DTOs;
using MediatR;

namespace Application.Commands
{
    public class UpdateRegionCommand : IRequest<RegionDto>
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; } = DateTime.Now;
    }
}
