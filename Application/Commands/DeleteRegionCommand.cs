using MediatR;

namespace Application.Commands
{
    public class DeleteRegionCommand : IRequest<bool>
    {
        public int RegionId { get; set; }
    }
}
