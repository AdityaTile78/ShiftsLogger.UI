using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.API.Models;
using ShiftsLogger.API.Services;

namespace ShiftsLogger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftsController : ControllerBase
    {

        private readonly IShiftService _shiftService;

        public ShiftsController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shift>>> GetAll()
        {
            var shifts = await _shiftService.GetAllShiftAsync();
            return Ok(shifts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shift>> Get(int id)
        {
            var shift = await _shiftService.GetShiftByIdAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            return Ok(shift);
        }

        [HttpPost]
        public async Task<ActionResult<Shift>> Create(Shift shift)
        {
            var createdShift = await _shiftService.AddShiftAsync(shift);
            return CreatedAtAction(nameof(Get), new { id = createdShift.Id }, createdShift);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Shift shift)
        {
            var success = await _shiftService.UpdateShiftAsync(id, shift);
            if (success == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _shiftService.DeleteShiftAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}
