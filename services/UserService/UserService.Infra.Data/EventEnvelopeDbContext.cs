using EventBus.Domain;
using Microsoft.EntityFrameworkCore;

namespace UserService.Infra.Data;

public class EventEnvelopeDbContext : DbContext
{
    public EventEnvelopeDbContext(DbContextOptions<EventEnvelopeDbContext> options)
        : base(options)
    {
    }

    public DbSet<EventEnvelopeEntity> EventEnvelopes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventEnvelopeEntity>(entity =>
        {
            entity.ToTable("Events"); 
            entity.HasKey(e => e.EventId);
            entity.Property(e => e.PayloadJson).IsRequired();
            entity.Property(e => e.MetadataJson).HasDefaultValue("{}");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}
