using EventBus.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Domain.Users;
using Newtonsoft.Json;
namespace UserService.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<EventEnvelope> EventEnvelopes => Set<EventEnvelope>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();


        modelBuilder.Entity<EventEnvelope>(envelope =>
        {
            envelope.ToTable("Events");
            envelope.HasKey(e => e.Id);
            envelope.Property(e => e.Payload).IsRequired();
            envelope.Property(e => e.Metadata)
             .HasConversion(
                 new ValueConverter<Dictionary<string, string>, string>(
                     v => JsonConvert.SerializeObject(v),
                     v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v) ?? new Dictionary<string, string>()
                 ))
             .HasColumnType("jsonb");
            envelope.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });


        base.OnModelCreating(modelBuilder);


    }
}