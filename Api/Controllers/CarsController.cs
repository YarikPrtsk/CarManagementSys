using Api.Dtos;
using Application.Common.Cars.Commands;
using Application.Common.Cars.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/cars")]
public class CarsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCar([FromBody] CreateCarRequest request)
    {
        var command = new CreateCarCommand(
            request.Make,
            request.Model,
            request.VinNumber,
            request.Color,
            request.Price,
            request.Year
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });
        
        return CreatedAtAction(
            nameof(GetCarById),
            new { id = result.Value },
            new { id = result.Value }
        );
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CarResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCars()
    {
        var query = new GetAllCarsQuery();
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        var response = result.Value?.Select(c => new CarResponse(
            c.Id,
            c.Make,
            c.Model,
            c.VinNumber,
            c.Color,
            c.Price,
            c.Status,
            c.Year,
            c.CreatedAt,
            c.UpdatedAt
        ));

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CarResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCarById(Guid id)
    {
        var query = new GetCarByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        var car = result.Value;
        var response = new CarResponse(
            car!.Id,
            car.Make,
            car.Model,
            car.VinNumber,
            car.Color,
            car.Price,
            car.Status,
            car.Year,
            car.CreatedAt,
            car.UpdatedAt
        );

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCar(Guid id, [FromBody] UpdateCarRequest request)
    {
        var command = new UpdateCarCommand(
            id,
            request.Make,
            request.Model,
            request.Color,
            request.Price
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCarStatus(Guid id, [FromBody] UpdateCarStatusRequest request)
    {
        var command = new UpdateCarStatusCommand(id, request.Status);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }
}