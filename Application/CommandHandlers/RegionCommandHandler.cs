using Application.Commands;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
namespace Application.CommandHandlers
{
    public class RegionCommandHandler :
        IRequestHandler<CreateRegionCommand, RegionDto>,
        IRequestHandler<UpdateRegionCommand, RegionDto>,
        IRequestHandler<DeleteRegionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RegionDto> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            var region = _mapper.Map<Region>(request);
            await _unitOfWork.Regions.AddAsync(region);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<RegionDto>(region);
        }

        public async Task<RegionDto> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
        {
            var region = await _unitOfWork.Regions.GetByIdAsync(request.RegionId);

            if (region == null)
            {
                throw new NotFoundException(nameof(Region), request.RegionId);
            }

            _mapper.Map(request, region);
            _unitOfWork.Regions.Update(region);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<RegionDto>(region);
        }

        public async Task<bool> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            var region = await _unitOfWork.Regions.GetByIdAsync(request.RegionId);

            if (region == null)
            {
                throw new NotFoundException(nameof(Region), request.RegionId);
            }

            _unitOfWork.Regions.Remove(region);
            var result = await _unitOfWork.CompleteAsync();
            return result > 0; // Return true if at least one change was saved
        }
    }
}
