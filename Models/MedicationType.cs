using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("MedicationType", Schema = "public")]
public class MedicationType
{
    [Key]
    [Column("MedicationTypeId")]
    public long MedicationTypeId { get; set; }

    [Column("MedicationTypeName")]
    public string MedicationTypeName { get; set; } = string.Empty;

    public ICollection<Medication> Medications { get; set; } = new List<Medication>();
}
