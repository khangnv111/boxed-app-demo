namespace BoxedApp.Services;

using MySqlConnector;

//using BoxedApp.Models;
//using MySql.Data.MySqlClient;

public class MySqlContext : IDisposable
{
    public MySqlConnection Connection { get; }

    public MySqlContext(string connectionString)
    {
        Connection = new MySqlConnection(connectionString);
    }

    public void Dispose() => Connection.Dispose();

    //private string ConnectionString { get; set; }

    //public MySqlContext(string connectionString)
    //{
    //    this.ConnectionString = connectionString;
    //}

    //private MySqlConnection GetConnection()
    //{
    //    return new MySqlConnection(ConnectionString);
    //}

    //public List<Book> GetAllBook()
    //{
    //    List<Book> list = new List<Book>();

    //    using (MySqlConnection conn = GetConnection())
    //    {
    //        conn.Open();
    //        MySqlCommand cmd = new MySqlCommand("SELECT * FROM books", conn);
    //        using (MySqlDataReader reader = cmd.ExecuteReader())
    //        {
    //            while (reader.Read())
    //            {
    //                list.Add(new Book()
    //                {
    //                    Id = reader.GetInt32("Id"),
    //                    Name = reader.GetString("NameBook")
    //                });
    //            }
    //        }
    //    }

    //    return list;
    //}
}
