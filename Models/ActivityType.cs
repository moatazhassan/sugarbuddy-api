using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("ActivityType", Schema = "public")]
public class ActivityType
{
    [Key]
    [Column("ActivityTypeId")]
    public long ActivityTypeId { get; set; }

    [Column("ActivityTypeName")]
    public string ActivityTypeName { get; set; } = string.Empty;

    public ICollection<Reading> Readings { get; set; } = new List<Reading>();
}
