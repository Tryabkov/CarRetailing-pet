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

        var result = await carService.CreateAsync(new CarEntity(userId, car), ct);
        
        if (result.Type == OperationResultType.Success) return Ok(result.Value);
        return BadRequest(result.ErrorMessage);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(uint id, CancellationToken ct)
    {
        var result = await carService.GetByIdAsync(id, ct);
        if (result.Type == OperationResultType.Success) return Ok(result.Value);
        if (result.Type == OperationResultType.NotFound) return NotFound(id);
        return BadRequest(result.ErrorMessage);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetCars([FromQuery] CarFilters parameters, CancellationToken ct)
    {
        var result = await carService.GetByFilterAsync(parameters, ct);
        
        if (result.Type == OperationResultType.Success) return Ok(result.Value);
        return BadRequest(result.ErrorMessage);
    }
    
    [Authorize]
    [HttpPatch("{id:int}")]
    [Consumes("application/json")]
    public async Task<IActionResult> Patch(uint id, [FromBody] UpdateCarDto car, CancellationToken ct)
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!uint.TryParse(userIdStr, out var userId)) return Unauthorized();
        var result = await carService.UpdateAsync(id, userId, car, ct);
        
        if (result.Type == OperationResultType.Success) return Ok(result.Value);
        return BadRequest(result.ErrorMessage);
    }
    
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(uint id, CancellationToken ct)
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!uint.TryParse(userIdStr, out var userId)) return Unauthorized();
        
        await carService.DeleteAsync(id, userId, ct);
        return Ok();
    }

}