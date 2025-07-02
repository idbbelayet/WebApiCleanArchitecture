using Application.CommandHandlers;
using Application.Commands;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Tests
{
    public class RegionCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IGenericRepository<Region>> _regionRepoMock;
        private readonly IMapper _mapper;
        private readonly RegionCommandHandler _handler;

        public RegionCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _regionRepoMock = new Mock<IGenericRepository<Region>>();
            _mapper = TestHelper.GetMapper();

            _unitOfWorkMock.Setup(u => u.Regions).Returns(_regionRepoMock.Object);
            _handler = new RegionCommandHandler(_unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task CreateRegion_ShouldReturnRegionDto()
        {
            // Arrange
            var command = new CreateRegionCommand
            {
                RegionName = "Asia",
                IsActive = true,
                UserId = 1,
                DataCollectionDate = DateTime.UtcNow
            };

            _regionRepoMock.Setup(r => r.AddAsync(It.IsAny<Region>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.RegionName.Should().Be("Asia");
        }

        [Fact]
        public async Task UpdateRegion_ShouldReturnUpdatedRegionDto()
        {
            // Arrange
            var existingRegion = new Region
            {
                RegionId = 1,
                RegionName = "Old Name",
                IsActive = true,
                UserId = 1,
                DataCollectionDate = DateTime.UtcNow
            };

            var updateCommand = new UpdateRegionCommand
            {
                RegionId = 1,
                RegionName = "New Name",
                IsActive = false,
                ModifiedBy = 2,
                ModificationDate = DateTime.UtcNow
            };

            _regionRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingRegion);
            _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(updateCommand, CancellationToken.None);

            // Assert
            result.RegionName.Should().Be("New Name");
            result.IsActive.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteRegion_ShouldReturnTrue_WhenSuccessful()
        {
            // Arrange
            var region = new Region { RegionId = 1, RegionName = "To Delete" };

            _regionRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(region);
            _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(new DeleteRegionCommand { RegionId = 1 }, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
        }
        [Fact]
        public async Task UpdateRegion_ShouldThrowNotFoundException_WhenRegionNotFound()
        {
            // Arrange
            _regionRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Region?)null);

            var command = new UpdateRegionCommand { RegionId = 99, RegionName = "Not Exist" };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteRegion_ShouldThrowNotFoundException_WhenRegionNotFound()
        {
            // Arrange
            _regionRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Region?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(new DeleteRegionCommand { RegionId = 99 }, CancellationToken.None));
        }


    }
}
