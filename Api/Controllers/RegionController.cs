using Application.Commands;
using Application.Common.Exceptions;
using Application.DTOs;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RegionDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllRegions()
        {
            var query = new GetAllRegionsQuery();
            var regions = await _mediator.Send(query);
            return Ok(regions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRegionById(int id)
        {
            try
            {
                var query = new GetRegionByIdQuery { RegionId = id };
                var region = await _mediator.Send(query);
                return Ok(region);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegionDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionCommand command)
        {
            var regionDto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.RegionId }, regionDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RegionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateRegion(int id, [FromBody] UpdateRegionCommand command)
        {
            if (id != command.RegionId)
            {
                return BadRequest("Region ID in URL does not match body.");
            }

            try
            {
                var regionDto = await _mediator.Send(command);
                return Ok(regionDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            try
            {
                var command = new DeleteRegionCommand { RegionId = id };
                var result = await _mediator.Send(command);
                if (result)
                {
                    return NoContent();
                }
                return NotFound(); // Should be caught by NotFoundException, but good to have fallback
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
