using Api.Dtos;
using Application.Common.CarSales.Commands;
using Application.Common.CarSales.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/car-sales")]
public class CarSalesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarSalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateCarSaleRequest request)
    {
        var command = new CreateCarSaleCommand(
            request.CarId,
            request.CustomerId,
            request.SaleNumber,
            request.CustomerName,
            request.CustomerContact,
            request.Title,
            request.Description,
            request.Priority,
            request.ScheduledDate
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return CreatedAtAction(
            nameof(GetSaleById),
            new { id = result.Value },
            new { id = result.Value }
        );
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CarSaleResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSales()
    {
        var query = new GetAllCarSalesQuery();
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        var response = result.Value?.Select(s => new CarSaleResponse(
            s.Id,
            s.SaleNumber,
            s.CarId,
            s.CustomerId,
            s.CustomerName,
            s.CustomerContact,
            s.Title,
            s.Description,
            s.Priority,
            s.Status,
            s.ScheduledDate,
            s.CompletedAt,
            s.CompletionNotes,
            s.CreatedAt,
            s.UpdatedAt
        ));

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CarSaleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById(Guid id)
    {
        var query = new GetCarSaleByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        var sale = result.Value;
        var response = new CarSaleResponse(
            sale!.Id,
            sale.SaleNumber,
            sale.CarId,
            sale.CustomerId,
            sale.CustomerName,
            sale.CustomerContact,
            sale.Title,
            sale.Description,
            sale.Priority,
            sale.Status,
            sale.ScheduledDate,
            sale.CompletedAt,
            sale.CompletionNotes,
            sale.CreatedAt,
            sale.UpdatedAt
        );

        return Ok(response);
    }
    
    [HttpGet("car/{carId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<CarSaleResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSalesByCarId(Guid carId)
    {
        var query = new GetCarSalesByCarIdQuery(carId);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        var response = result.Value?.Select(s => new CarSaleResponse(
            s.Id,
            s.SaleNumber,
            s.CarId,
            s.CustomerId,
            s.CustomerName,
            s.CustomerContact,
            s.Title,
            s.Description,
            s.Priority,
            s.Status,
            s.ScheduledDate,
            s.CompletedAt,
            s.CompletionNotes,
            s.CreatedAt,
            s.UpdatedAt
        ));

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateCarSaleRequest request)
    {
        var command = new UpdateCarSaleCommand(
            id,
            request.Title,
            request.Description,
            request.Priority,
            request.ScheduledDate
        );

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpPatch("{id:guid}/start")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> StartSale(Guid id)
    {
        var command = new StartCarSaleCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CompleteSale(Guid id, [FromBody] CompleteCarSaleRequest? request)
    {
        var command = new CompleteCarSaleCommand(id, request?.CompletionNotes ?? string.Empty);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }

    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSale(Guid id)
    {
        var command = new CancelCarSaleCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Error });

        return NoContent();
    }
}