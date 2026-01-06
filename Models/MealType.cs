using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("MealType", Schema = "public")]
public class MealType
{
    [Key]
    [Column("MealTypeId")]
    public long MealTypeId { get; set; }

    [Column("MealTypeName")]
    public string MealTypeName { get; set; } = string.Empty;

    public ICollection<Reading> Readings { get; set; } = new List<Reading>();
}
