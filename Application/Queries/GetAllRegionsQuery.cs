using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public class GetAllRegionsQuery : IRequest<IEnumerable<RegionDto>>
    {
    }
}
