using Api.Controllers;
using Application.Commands;
using Application.Common.Exceptions;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Tests.Tests
{


    public class RegionControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly RegionController _controller;

        public RegionControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new RegionController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllRegions_ShouldReturnOkWithList()
        {
            // Arrange
            var regionList = new List<RegionDto> { new() { RegionId = 1, RegionName = "Asia" } };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllRegionsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(regionList);

            // Act
            var result = await _controller.GetAllRegions();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResult.Value.Should().BeAssignableTo<IEnumerable<RegionDto>>();
        }

        [Fact]
        public async Task GetRegionById_ShouldReturnOk_WhenRegionExists()
        {
            // Arrange
            var region = new RegionDto { RegionId = 1, RegionName = "Asia" };
            _mediatorMock.Setup(m => m.Send(It.Is<GetRegionByIdQuery>(q => q.RegionId == 1), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(region);

            // Act
            var result = await _controller.GetRegionById(1);

            // Assert
            var okResult = result as OkObjectResult;
            okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResult.Value.Should().Be(region);
        }

        [Fact]
        public async Task GetRegionById_ShouldReturnNotFound_WhenRegionDoesNotExist()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRegionByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new NotFoundException(nameof(Region), 99));

            // Act
            var result = await _controller.GetRegionById(99);

            // Assert
            var notFound = result as NotFoundObjectResult;
            notFound!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateRegion_ShouldReturnCreatedAt_WhenValid()
        {
            // Arrange
            var command = new CreateRegionCommand { RegionName = "Africa", IsActive = true };
            var regionDto = new RegionDto { RegionId = 2, RegionName = "Africa" };

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(regionDto);

            // Act
            var result = await _controller.CreateRegion(command);

            // Assert
            var createdAt = result as CreatedAtActionResult;
            createdAt!.StatusCode.Should().Be((int)HttpStatusCode.Created);
            createdAt.Value.Should().Be(regionDto);
        }

        [Fact]
        public async Task UpdateRegion_ShouldReturnBadRequest_WhenIdsMismatch()
        {
            // Arrange
            var command = new UpdateRegionCommand { RegionId = 2, RegionName = "Updated" };

            // Act
            var result = await _controller.UpdateRegion(1, command);

            // Assert
            var badRequest = result as BadRequestObjectResult;
            badRequest!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateRegion_ShouldReturnOk_WhenValid()
        {
            // Arrange
            var command = new UpdateRegionCommand { RegionId = 1, RegionName = "Updated" };
            var updatedRegion = new RegionDto { RegionId = 1, RegionName = "Updated" };

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(updatedRegion);

            // Act
            var result = await _controller.UpdateRegion(1, command);

            // Assert
            var okResult = result as OkObjectResult;
            okResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResult.Value.Should().Be(updatedRegion);
        }

        [Fact]
        public async Task UpdateRegion_ShouldReturnNotFound_WhenNotExists()
        {
            // Arrange
            var command = new UpdateRegionCommand { RegionId = 1, RegionName = "Updated" };

            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new NotFoundException(nameof(Region), 1));

            // Act
            var result = await _controller.UpdateRegion(1, command);

            // Assert
            var notFound = result as NotFoundObjectResult;
            notFound!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteRegion_ShouldReturnNoContent_WhenDeleted()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteRegionCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteRegion(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteRegion_ShouldReturnNotFound_WhenRegionDoesNotExist()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteRegionCommand>(), It.IsAny<CancellationToken>()))
                         .ThrowsAsync(new NotFoundException(nameof(Region), 1));

            // Act
            var result = await _controller.DeleteRegion(1);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }


}
