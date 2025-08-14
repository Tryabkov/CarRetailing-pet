using System.Security.Claims;
using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/cars")]
public class CarsController(ICarService carService) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCarDto car, CancellationToken ct)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        uint userId = uint.TryParse(claim?.Value, out var id)
            ? id
            : throw new UnauthorizedAccessException("Invalid user ID claim");

        await carService.CreateAsync(new CarEntity(userId, car), ct);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery]uint id, CancellationToken ct)
    {
        var car = await carService.GetByIdAsync(id, ct);
        if (car is null) return NotFound();
        return Ok(car);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetCars([FromQuery] CarFilters parameters, CancellationToken ct)
    {
        return Ok(await carService.GetByFilterAsync(parameters, ct));
    }

    [HttpPatch]
    public async Task<IActionResult> Patch([FromBody] CarEntity car, CancellationToken ct)
    {
        await carService.UpdateAsync(car, ct);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] uint id, CancellationToken ct)
    {
        await carService.DeleteAsync(id, ct);
        return Ok();
    }

}