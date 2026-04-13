namespace Example.Services;

using Example.Accessors;

public sealed class DataService
{
    private readonly ILogger<DataService> log;

    private readonly IDialect dialect;

    private readonly IDataAccessor dataAccessor;

    public DataService(
        ILogger<DataService> log,
        IDialect dialect,
        IAccessorResolver<IDataAccessor> dataResolver)
    {
        this.log = log;
        this.dialect = dialect;
        dataAccessor = dataResolver.Accessor;
    }

    public async ValueTask<bool> InsertDataAsync(DataEntity entity)
    {
        try
        {
            await dataAccessor.InsertDataAsync(entity).ConfigureAwait(false);
            return true;
        }
        catch (DbException ex)
        {
            if (dialect.IsDuplicate(ex))
            {
                log.WarnInsertDuplicated(entity.Id);
                return false;
            }

            throw;
        }
    }

    public ValueTask<DataEntity?> QueryDataAsync(int id) =>
        dataAccessor.QueryDataAsync(id);

    public ValueTask<List<DataEntity>> QueryDataListAsync(bool? flag, int limit, int offset) =>
        dataAccessor.QueryDataListAsync(flag, limit, offset);

    public ValueTask<int> CountDataAsync(bool? flag) =>
        dataAccessor.CountDataAsync(flag);
}
