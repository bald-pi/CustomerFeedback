namespace FeedbackSubmission.API.Database;

internal class HistoryTableRepository : NpgsqlHistoryRepository
{
    public HistoryTableRepository(HistoryRepositoryDependencies dependencies) : base(dependencies) { }

    protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> history)
    {
        history.Property(property => property.MigrationId).HasColumnName(Constants.MIGRATION_ID_COLUMN_NAME);
        history.Property(h => h.ProductVersion).HasColumnName(Constants.PRODUCT_VERSION_COLUMN_NAME);
    }
}
