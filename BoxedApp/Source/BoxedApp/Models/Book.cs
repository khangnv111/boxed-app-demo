namespace BoxedApp.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTimeOffset Modified { get; set; }
    public DateTimeOffset Created { get; set; }
}

public class BookDB
{
    public int Id { get; set; }
    public string NameBook { get; set; } = default!;
}

public class BookStore
{
    /// <summary>
    /// Id
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    /// <summary>
    /// Name book
    /// </summary>
    /// <sample>Book 1</sample>
    [BsonElement("name")]
    public string BookName { get; set; } = null!;

    /// <summary>
    /// Author
    /// </summary>
    /// <sample>Author 1</sample>
    public string author { get; set; } = null!;
}
