namespace BoxedApp.Repositories;

using BoxedApp.Models;

public interface ISqlRepository
{
    Task<List<BookDB>> GetBookSqlAsync(CancellationToken cancellationToken);
}
