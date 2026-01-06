using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class MealTypesController : ControllerBase
{
    private readonly SugarDbContext _context;

    public MealTypesController(SugarDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MealType>>> GetMealTypes()
    {
        return await _context.MealTypes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MealType>> GetMealType(long id)
    {
        var mealType = await _context.MealTypes.FindAsync(id);
        if (mealType == null) return NotFound();
        return mealType;
    }

    [HttpPost]
    public async Task<ActionResult<MealType>> CreateMealType([FromBody] MealType mealType)
    {
        // خلي EF يولد الـ ID تلقائي
        mealType.MealTypeId = 0;

        _context.MealTypes.Add(mealType);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { message = ex.InnerException?.Message });
        }

        return CreatedAtAction(nameof(GetMealType), new { id = mealType.MealTypeId }, mealType);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMealType(long id, MealType mealType)
    {
        if (id != mealType.MealTypeId) return BadRequest();
        _context.Entry(mealType).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMealType(long id)
    {
        var mealType = await _context.MealTypes.FindAsync(id);
        if (mealType == null) return NotFound();
        _context.MealTypes.Remove(mealType);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
