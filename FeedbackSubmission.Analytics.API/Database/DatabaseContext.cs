using FeedbackSubmission.Analytics.API.Features.Analytics.Database;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace FeedbackSubmission.Analytics.API.Database;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Analytic> Analytics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Analytic>().ToCollection("analytics");
    }
}
