using EventBus.Domain;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Users;

namespace UserService.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<EventEnvelopeEntity> EventEnvelopes => Set<EventEnvelopeEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();


        modelBuilder.Entity<EventEnvelopeEntity>(entity =>
        {
            entity.ToTable("UserEvents");
            entity.HasKey(e => e.EventId);
            entity.Property(e => e.PayloadJson).IsRequired();
            entity.Property(e => e.MetadataJson).HasDefaultValue("{}");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });


        base.OnModelCreating(modelBuilder);


    }
}