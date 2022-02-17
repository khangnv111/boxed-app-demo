namespace BoxedApp.Repositories;

using System.Collections.Generic;
using System.Data.Common;
using BoxedApp.Models;

public interface IBookRepository
{

    Task<Book> AddAsync(Book book, CancellationToken cancellationToken);
    Task<Book?> GetAsync(int bookId, CancellationToken cancellationToken);
    Task<List<BookDB>> GetBookSqlAsync(CancellationToken cancellationToken);
    Task<List<BookStore>> GetBookStoreAsync(CancellationToken cancellationToken);
}
