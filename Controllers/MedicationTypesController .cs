using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class MedicationTypesController : ControllerBase
{
    private readonly SugarDbContext _context;

    public MedicationTypesController(SugarDbContext context)
    {
        _context = context;
    }

    // GET: api/MedicationTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicationType>>> GetMedicationTypes()
    {
        return await _context.MedicationTypes.ToListAsync();
    }

    // GET: api/MedicationTypes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MedicationType>> GetMedicationType(long id)
    {
        var medType = await _context.MedicationTypes.FindAsync(id);
        if (medType == null) return NotFound();
        return medType;
    }

    // POST: api/MedicationTypes
    [HttpPost]
    public async Task<ActionResult<MedicationType>> CreateMedicationType([FromBody] MedicationType medicationType)
    {
        // خلي EF يولد الـ ID تلقائي
        medicationType.MedicationTypeId = 0;

        _context.MedicationTypes.Add(medicationType);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { message = ex.InnerException?.Message });
        }

        return CreatedAtAction(nameof(GetMedicationType), new { id = medicationType.MedicationTypeId }, medicationType);
    }


    // PUT: api/MedicationTypes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMedicationType(long id, MedicationType medType)
    {
        if (id != medType.MedicationTypeId) return BadRequest();

        _context.Entry(medType).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.MedicationTypes.Any(m => m.MedicationTypeId == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/MedicationTypes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedicationType(long id)
    {
        var medType = await _context.MedicationTypes.FindAsync(id);
        if (medType == null) return NotFound();

        _context.MedicationTypes.Remove(medType);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
