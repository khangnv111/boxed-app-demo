namespace BoxedApp.Repositories;

using BoxedApp.Models;
using BoxedApp.Services;

public class SqlRepository : ISqlRepository
{
    private readonly SqlDBContext sqlDBContext;
    public SqlRepository(SqlDBContext sqlDBContext)
    {
        this.sqlDBContext = sqlDBContext;
    }

    public Task<List<BookDB>> GetBookSqlAsync(CancellationToken cancellationToken)
    {
        var list = this.sqlDBContext.Books.ToList();
        if (list is null)
        {
            return Task.FromResult(new List<BookDB>());
        }
        return Task.FromResult(list);
    }
}
