using Application.DTOs;
using MediatR;

namespace Application.Commands
{
    public class CreateRegionCommand : IRequest<RegionDto>
    {
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public DateTime DataCollectionDate { get; set; } = DateTime.Now;
    }
}
