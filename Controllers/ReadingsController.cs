using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ReadingsController : ControllerBase
{
    private readonly SugarDbContext _context;

    public ReadingsController(SugarDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reading>>> GetReadings()
    {
        return await _context.Readings
            .Include(r => r.User)
            .Include(r => r.MealType)
            .Include(r => r.Medication)
            .Include(r => r.ActivityType)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reading>> GetReading(long id)
    {
        var reading = await _context.Readings
            .Include(r => r.User)
            .Include(r => r.MealType)
            .Include(r => r.Medication)
            .Include(r => r.ActivityType)
            .FirstOrDefaultAsync(r => r.ReadingId == id);

        if (reading == null) return NotFound();
        return reading;
    }

    [HttpPost]
    public async Task<ActionResult<Reading>> CreateReading(Reading reading)
    {

        // خلي EF يولد الـ ID تلقائي
        reading.ReadingId = 0;

        _context.Readings.Add(reading);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { message = ex.InnerException?.Message });
        }

        return CreatedAtAction(nameof(GetReading), new { id = reading.ReadingId }, reading);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReading(long id, Reading reading)
    {
        if (id != reading.ReadingId) return BadRequest();

        _context.Entry(reading).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Readings.Any(r => r.ReadingId == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReading(long id)
    {
        var reading = await _context.Readings.FindAsync(id);
        if (reading == null) return NotFound();

        _context.Readings.Remove(reading);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
