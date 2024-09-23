namespace FeedbackSubmission.API.Database
{
    public class DatabaseContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Features.Feedback.Entities.Feedback> Feedbacks { get; set; }
    }
}