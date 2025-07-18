using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Cars")]
    public class CarsController(ICarService carService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarEntity car, CancellationToken ct)
        {
            await carService.CreateAsync(car, ct);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]uint id, CancellationToken ct)
        {
            var car = await carService.GetByIdAsync(id, ct);
            if (car is null) return NotFound();
            return Ok(car);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            return Ok(await carService.GetAllAsync(ct));
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
