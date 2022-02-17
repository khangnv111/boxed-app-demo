namespace BoxedApp.Repositories;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using BoxedApp.Models;
using BoxedApp.Services;
using MySql.Data.MySqlClient;

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
    private readonly IConfiguration configuration;
    private MySqlContext db { get; }
    public BookRepository(MongoDBService mogoDBService, IConfiguration configuration, MySqlContext db)
    {
        this.mogoDBService = mogoDBService;
        this.configuration = configuration;
        this.db = db;
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

    public async Task<List<BookDB>> GetBookSqlAsync(CancellationToken cancellationToken)
    {

        var ConnectionString = this.configuration.GetConnectionString("MySqlConnection");

        var list = new List<BookDB>();
        //await db.Connection.OpenAsync();
        //var query = new BookQuery(db);
        //var list = await query.GetAll("");

        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM books", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new BookDB()
                    {
                        Id = reader.GetInt32("Id"),
                        NameBook = reader.GetString("NameBook")
                    });
                }
            }
        }

        return await Task.FromResult(list);
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

    #region convert table
    private static List<T> ConvertDataTable<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }

    private static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                    pro.SetValue(obj, dr[column.ColumnName], null);
                else
                    continue;
            }
        }
        return obj;
    }
    #endregion
}
