namespace BoxedApp.Models;

using System.Data.Common;
using BoxedApp.Services;

public class BookQuery
{
    public MySqlContext Db { get; }

    public BookQuery(MySqlContext db)
    {
        Db = db;
    }

    public async Task<List<BookDB>> GetAll(string command)
    {
        using var cmd = Db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * from books";
        return await ReadAllAsync(await cmd.ExecuteReaderAsync());
    }

    private async Task<List<BookDB>> ReadAllAsync(DbDataReader reader)
    {
        var posts = new List<BookDB>();
        using (reader)
        {
            while (await reader.ReadAsync())
            {
                var post = new BookDB()
                {
                    Id = reader.GetInt32(0),
                    NameBook = reader.GetString(1)
                };
                posts.Add(post);
            }
        }
        return posts;
    }
}
