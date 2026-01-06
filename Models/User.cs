using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("User", Schema = "public")]
public class User
{
    [Key]
    [Column("UserId")]
    public long UserId { get; set; }

    [Column("UserName")]
    public string UserName { get; set; } = string.Empty;

    [Column("UserEmail")]
    public string UserEmail { get; set; } = string.Empty;

    [Column("UserBirthday")]
    public DateTime? UserBirthday { get; set; }

    [Column("UserTypeOfDiabets")]
    public string? UserTypeOfDiabets { get; set; }

    [Column("UserPassword")]
    public string? UserPassword { get; set; }

    [Column("UserPhoneNumber")]
    public string? UserPhoneNumber { get; set; }

    public ICollection<Reading> Readings { get; set; } = new List<Reading>();
    public ICollection<Medication> Medications { get; set; } = new List<Medication>();
}
