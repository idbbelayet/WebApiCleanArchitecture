using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class RegionQueryHandler :
        IRequestHandler<GetRegionByIdQuery, RegionDto>,
        IRequestHandler<GetAllRegionsQuery, IEnumerable<RegionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RegionDto> Handle(GetRegionByIdQuery request, CancellationToken cancellationToken)
        {
            var region = await _unitOfWork.Regions.GetByIdAsync(request.RegionId);

            if (region == null)
            {
                throw new NotFoundException(nameof(Region), request.RegionId);
            }

            return _mapper.Map<RegionDto>(region);
        }

        public async Task<IEnumerable<RegionDto>> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
        {
            var regions = await _unitOfWork.Regions.GetAllAsync();
            return _mapper.Map<IEnumerable<RegionDto>>(regions);
        }
    }
}
