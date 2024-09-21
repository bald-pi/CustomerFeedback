namespace FeedbackSubmission.API.Database;

internal class HistoryTableRepository : NpgsqlHistoryRepository
{
    public HistoryTableRepository(HistoryRepositoryDependencies dependencies) : base(dependencies) { }

    protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> history)
    {
        history.Property(property => property.MigrationId).HasColumnName("MigrationId");
        history.Property(h => h.ProductVersion).HasColumnName("ProductVersion");
    }
}
