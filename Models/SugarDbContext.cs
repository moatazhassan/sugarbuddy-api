using Microsoft.EntityFrameworkCore;

public class SugarDbContext : DbContext
{
    public SugarDbContext(DbContextOptions<SugarDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Reading> Readings { get; set; }
    public DbSet<MedicationType> MedicationTypes { get; set; }
    public DbSet<Medication> Medications { get; set; }
    public DbSet<MealType> MealTypes { get; set; }
    public DbSet<ActivityType> ActivityTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        // User Primary Key
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        // Reading → User
        modelBuilder.Entity<Reading>()
            .HasOne(r => r.User)
            .WithMany(u => u.Readings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Reading → MealType
        modelBuilder.Entity<Reading>()
            .HasOne(r => r.MealType)
            .WithMany(m => m.Readings)
            .HasForeignKey(r => r.MealTypeId)
            .OnDelete(DeleteBehavior.Cascade);


        // Reading → Medication
        modelBuilder.Entity<Reading>()
            .HasOne(r => r.Medication)
            .WithMany(m => m.Readings)
            .HasForeignKey(r => r.MedicationId) // مطابق للـ DB
            .OnDelete(DeleteBehavior.Cascade);

        // Reading → ActivityType
        modelBuilder.Entity<Reading>()
            .HasOne(r => r.ActivityType)
            .WithMany(a => a.Readings)
            .HasForeignKey(r => r.ActivityTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Medication → MedicationType
        modelBuilder.Entity<Medication>()
            .HasOne(m => m.MedicationType)
            .WithMany(mt => mt.Medications)
            .HasForeignKey(m => m.MedicationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Medication → User
        modelBuilder.Entity<Medication>()
            .HasOne(m => m.User)
            .WithMany(u => u.Medications)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
