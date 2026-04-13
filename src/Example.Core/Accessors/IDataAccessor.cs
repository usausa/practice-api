namespace Example.Accessors;

[DataAccessor]
public interface IDataAccessor
{
    [Insert]
    ValueTask InsertDataAsync(DataEntity entity);

    [QueryFirstOrDefault]
    ValueTask<DataEntity?> QueryDataAsync(int id);

    [Query]
    ValueTask<List<DataEntity>> QueryDataListAsync(bool? flag, int limit, int offset);

    [ExecuteScalar]
    ValueTask<int> CountDataAsync(bool? flag);
}
