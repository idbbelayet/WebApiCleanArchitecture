using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public class GetRegionByIdQuery : IRequest<RegionDto>
    {
        public int RegionId { get; set; }
    }
}
