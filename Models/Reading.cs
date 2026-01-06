using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("Reading", Schema = "public")]
public class Reading
{
    [Key]
    [Column("ReadingId")]
    public long ReadingId { get; set; }

    [Column("UserId")]
    public long? UserId { get; set; }

    [Column("Date")]
    public DateTime? Date { get; set; }

    [Column("Time")]
    public TimeSpan? Time { get; set; }

    [Column("ReadingValue")]
    public decimal? ReadingValue { get; set; }

    [Column("MealTypeId")]
    public long? MealTypeId { get; set; }

    [Column("MedicationId")] // مطابق للـ DB
    public long? MedicationId { get; set; }

    [Column("MeadicationDosage")]
    public decimal? MedicationDosage { get; set; }

    [Column("MealDescription")]
    public string? MealDescription { get; set; }

    [Column("MealCarbs")]
    public decimal? MealCarbs { get; set; }

    [Column("MealCalories")]
    public decimal? MealCalories { get; set; }

    [Column("ActivityTypeId")]
    public long? ActivityTypeId { get; set; }

    [Column("ActivityDurationMinutes")]
    public decimal? ActivityDurationMinutes { get; set; }

    [Column("ActivityIntensity")]
    public string? ActivityIntensity { get; set; }

    [Column("Notes")]
    public string? Notes { get; set; }

    public User? User { get; set; }
    public MealType? MealType { get; set; }
    public Medication? Medication { get; set; }
    public ActivityType? ActivityType { get; set; }
}
