using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class MedicationsController : ControllerBase
{
    private readonly SugarDbContext _context;

    public MedicationsController(SugarDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Medication>>> GetMedications()
    {
        return await _context.Medications
            .Include(m => m.User)
            .Include(m => m.MedicationType)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Medication>> GetMedication(long id)
    {
        var med = await _context.Medications
            .Include(m => m.User)
            .Include(m => m.MedicationType)
            .FirstOrDefaultAsync(m => m.MedicationId == id);

        if (med == null) return NotFound();
        return med;
    }

    [HttpPost]
    public async Task<ActionResult<Medication>> CreateMedication(Medication medication)
 
    {
        if (medication == null)
            return BadRequest("No medication data provided");

        // لا تضبط MedicationId → EF Core يولده تلقائيًا
        _context.Medications.Add(medication);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { message = ex.InnerException?.Message });
        }

        return CreatedAtAction(nameof(GetMedication), new { id = medication.MedicationId }, medication);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMedication(long id, Medication med)
    {
        if (id != med.MedicationId) return BadRequest();
        _context.Entry(med).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Medications.Any(m => m.MedicationId == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedication(long id)
    {
        var med = await _context.Medications.FindAsync(id);
        if (med == null) return NotFound();

        _context.Medications.Remove(med);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
