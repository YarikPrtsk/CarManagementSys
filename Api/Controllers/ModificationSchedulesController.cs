using Api.Dtos;
using Application.Common.ModificationSchedules.Commands;
using Application.Common.ModificationSchedules.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/modification-schedules")]
public class ModificationSchedulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ModificationSchedulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateModificationScheduleRequest request)
    {
        var command = new CreateModificationScheduleCommand(
            request.CarId,
            request.TaskName,
            request.Description,
            request.Frequency,
            request.NextDueDate
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return CreatedAtAction(
            nameof(GetScheduleById),
            new { id = result.Value },
            new { id = result.Value }
        );
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ModificationScheduleResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSchedules()
    {
        var query = new GetAllModificationSchedulesQuery();
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        var response = result.Value?.Select(s => new ModificationScheduleResponse(
            s.Id,
            s.CarId,
            s.TaskName,
            s.Description,
            s.Frequency,
            s.NextDueDate,
            s.IsActive,
            s.CreatedAt,
            s.UpdatedAt
        ));

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ModificationScheduleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetScheduleById(Guid id)
    {
        var query = new GetModificationScheduleByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        var schedule = result.Value;
        var response = new ModificationScheduleResponse(
            schedule!.Id,
            schedule.CarId,
            schedule.TaskName,
            schedule.Description,
            schedule.Frequency,
            schedule.NextDueDate,
            schedule.IsActive,
            schedule.CreatedAt,
            schedule.UpdatedAt
        );

        return Ok(response);
    }

    [HttpGet("car/{carId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<ModificationScheduleResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSchedulesByCarId(Guid carId)
    {
        var query = new GetModificationSchedulesByCarIdQuery(carId);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        var response = result.Value?.Select(s => new ModificationScheduleResponse(
            s.Id,
            s.CarId,
            s.TaskName,
            s.Description,
            s.Frequency,
            s.NextDueDate,
            s.IsActive,
            s.CreatedAt,
            s.UpdatedAt
        ));

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSchedule(Guid id, [FromBody] UpdateModificationScheduleRequest request)
    {
        var command = new UpdateModificationScheduleCommand(
            id,
            request.TaskName,
            request.Description,
            request.Frequency,
            request.NextDueDate
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateSchedule(Guid id)
    {
        var command = new DeactivateModificationScheduleCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpPost("{id:guid}/reactivate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReactivateSchedule(Guid id, [FromBody] ReactivateModificationScheduleRequest request)
    {
        var command = new ReactivateModificationScheduleCommand(id, request.NextDueDate);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }
}