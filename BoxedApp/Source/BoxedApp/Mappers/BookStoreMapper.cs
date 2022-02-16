namespace BoxedApp.Mappers;

using Boxed.Mapping;
using BoxedApp.Models;

public class BookStoreMapper : IMapper<BookStore, BookStore>
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly LinkGenerator linkGenerator;

    public BookStoreMapper(
        IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.linkGenerator = linkGenerator;
    }

    public void Map(BookStore source, BookStore destination)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);

        destination.Id = source.Id;
        destination.BookName = source.BookName;
        destination.author = source.author;
        //destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
        //    this.httpContextAccessor.HttpContext!,
        //    BookControllerRoute.GetBook,
        //    new { source.Id })!);
    }
}
