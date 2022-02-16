namespace BoxedApp.ViewModels;

using System.Text.Json.Serialization;

/// <summary>
/// Enables faster serialization and de-serialization with fewer allocations by generating source code.
/// </summary>
[JsonSerializable(typeof(Car[]))]
[JsonSerializable(typeof(Connection<Car>[]))]
[JsonSerializable(typeof(SaveCar[]))]
[JsonSerializable(typeof(Book[]))]
[JsonSerializable(typeof(Connection<Book>[]))]
[JsonSerializable(typeof(SaveBook[]))]
internal partial class CustomJsonSerializerContext : JsonSerializerContext
{
}
