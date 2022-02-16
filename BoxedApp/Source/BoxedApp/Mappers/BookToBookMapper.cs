namespace BoxedApp.Mappers;

using Boxed.Mapping;
using BoxedApp.Constants;
using BoxedApp.ViewModels;

public class BookToBookMapper : IMapper<Models.Book, Book>
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly LinkGenerator linkGenerator;

    public BookToBookMapper(
        IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.linkGenerator = linkGenerator;
    }

    public void Map(Models.Book source, Book destination)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);

        destination.Id = source.Id;
        destination.NameBook = source.Name;
        //destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
        //    this.httpContextAccessor.HttpContext!,
        //    BookControllerRoute.GetBook,
        //    new { source.Id })!);
    }
}
