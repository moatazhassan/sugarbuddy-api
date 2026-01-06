using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("Medication", Schema = "public")]
public class Medication
{
    [Key]
    [Column("MedicationId")]
    public long MedicationId { get; set; }

    [Column("UserId")]
    public long? UserId { get; set; }

    [Column("MedicationTypeId")]
    public long? MedicationTypeId { get; set; }

    [Column("MedicationName")]
    public string? MedicationName { get; set; }

    public User? User { get; set; }
    public MedicationType? MedicationType { get; set; }
    public ICollection<Reading> Readings { get; set; } = new List<Reading>();


}
