namespace BoxedApp.Mappers;

using Boxed.Mapping;
using BoxedApp.Services;
using BoxedApp.ViewModels;

public class BookToSaveBookMapper : IMapper<Models.Book, SaveBook>, IMapper<SaveBook, Models.Book>
{
    private readonly IClockService clockService;

    public BookToSaveBookMapper(IClockService clockService) =>
        this.clockService = clockService;

    public void Map(Models.Book source, SaveBook destination)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);

        destination.NameBook = source.Name;
    }

    public void Map(SaveBook source, Models.Book destination)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);

        var now = this.clockService.UtcNow;

        if (destination.Created == DateTimeOffset.MinValue)
        {
            destination.Created = now;
        }

        destination.Name = source.NameBook;
        destination.Modified = now;
    }
}
