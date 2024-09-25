using FeedbackSubmission.Analytics.API.Features.Summary.Entities;

namespace FeedbackSubmission.Analytics.API.Database;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Summary> Summaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Summary>().ToCollection("feedback_summary");
    }
}
