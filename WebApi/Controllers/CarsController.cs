using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController(ICarService carService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarDto car, CancellationToken ct)
        { 
            await carService.CreateAsync(new CarEntity(car), ct);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]uint id, CancellationToken ct)
        {
            var car = await carService.GetPublicCarAsync(id, ct);
            if (car is null) return NotFound();
            return Ok(car);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            return Ok(await carService.GetAllPublicCarsAsync(ct));
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] CarEntity car, CancellationToken ct)
        {
            await carService.UpdateAsync(car, ct);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Patch([FromQuery] uint id, CancellationToken ct)
        {
            await carService.DeleteAsync(id, ct);
            return Ok();
        }

    }
}
