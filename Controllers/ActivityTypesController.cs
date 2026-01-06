using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ActivityTypesController : ControllerBase
{
    private readonly SugarDbContext _context;

    public ActivityTypesController(SugarDbContext context)
    {
        _context = context;
    }

    // GET: api/ActivityTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityType>>> GetActivityTypes()
    {
        return await _context.ActivityTypes.ToListAsync();
    }

    // GET: api/ActivityTypes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityType>> GetActivityType(long id)
    {
        var activityType = await _context.ActivityTypes.FindAsync(id);
        if (activityType == null) return NotFound();
        return activityType;
    }

    // POST: api/ActivityTypes
    [HttpPost]
    public async Task<ActionResult<ActivityType>> CreateActivityType(ActivityType activityType)
    {
        // خلي EF يولد الـ ID تلقائي
        activityType.ActivityTypeId = 0;

        _context.ActivityTypes.Add(activityType);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { message = ex.InnerException?.Message });
        }

        return CreatedAtAction(nameof(GetActivityType), new { id = activityType.ActivityTypeId }, activityType);
    }

    // PUT: api/ActivityTypes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivityType(long id, ActivityType activityType)
    {
        if (id != activityType.ActivityTypeId) return BadRequest();

        _context.Entry(activityType).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ActivityTypes.Any(a => a.ActivityTypeId == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/ActivityTypes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivityType(long id)
    {
        var activityType = await _context.ActivityTypes.FindAsync(id);
        if (activityType == null) return NotFound();

        _context.ActivityTypes.Remove(activityType);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
