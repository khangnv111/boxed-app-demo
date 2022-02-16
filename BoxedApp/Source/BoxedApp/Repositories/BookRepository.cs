namespace BoxedApp.Repositories;
using BoxedApp.Models;
using BoxedApp.Services;

//using BoxedApp.Services;

public class BookRepository : IBookRepository
{
    private static readonly List<Book> Books = new()
    {
        new Book()
        {
            Id = 1,
            Created = DateTimeOffset.UtcNow.AddDays(-8),
            Name = "Book 1",
            Modified = DateTimeOffset.UtcNow.AddDays(-8),
        },
        new Book()
        {
            Id = 2,
            Created = DateTimeOffset.UtcNow.AddDays(-5),
            Name = "Furai",
            Modified = DateTimeOffset.UtcNow.AddDays(-6),
        }
    };

    //private readonly IMapper<BookStore, BookStore> bookStoreMapper;
    private readonly MongoDBService mogoDBService;

    public BookRepository(MongoDBService mogoDBService)
    {
        this.mogoDBService = mogoDBService;
    }

    public Task<Book> AddAsync(Book book, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(book);

        Books.Add(book);
        book.Id = Books.Max(x => x.Id) + 1;
        return Task.FromResult(book);
    }

    public Task<Book?> GetAsync(int bookId, CancellationToken cancellationToken)
    {
        var book = Books.FirstOrDefault(x => x.Id == bookId);
        return Task.FromResult(book);
    }

    public Task<List<BookDB>> GetBookSqlAsync(CancellationToken cancellationToken)
    {
        //var list = this.sqlDBContext.Books.ToList();
        //if (list is null)
        //{
        //    return Task.FromResult(new List<BookDB>());
        //}
        return Task.FromResult(new List<BookDB>());
    }

    public Task<List<BookStore>> GetBookStoreAsync(CancellationToken cancellationToken)
    {
        var list = this.mogoDBService.GetBookStoreAsync();
        if (list is null)
        {
            return Task.FromResult(new List<BookStore>());
        }

        return Task.FromResult(list.Result.ToList());
    }
}
