namespace BoxedApp.Commands;

using System.Globalization;
using Boxed.Mapping;
using BoxedApp.Repositories;
using BoxedApp.Services;
using BoxedApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public class GetBookCommand
{
    private readonly IActionContextAccessor actionContextAccessor;
    private readonly IBookRepository bookRepository;
    //private readonly ISqlRepository sqlRepository;
    private readonly IMapper<Models.Book, Book> bookMapper;

    //private readonly MySqlContext db;

    public GetBookCommand(IBookRepository bookRepository, IActionContextAccessor actionContextAccessor, IMapper<Models.Book, Book> bookMapper)
    {
        this.bookRepository = bookRepository;
        this.actionContextAccessor = actionContextAccessor;
        this.bookMapper = bookMapper;
    }

    public async Task<IActionResult> ExecuteAsync(int bookId, CancellationToken cancellationToken)
    {
        var book = await this.bookRepository.GetAsync(bookId, cancellationToken).ConfigureAwait(false);
        if (book is null)
        {
            return new NotFoundResult();
        }

        var httpContext = this.actionContextAccessor.ActionContext!.HttpContext;
        var ifModifiedSince = httpContext.Request.Headers.IfModifiedSince;
        if (ifModifiedSince.Any() &&
            DateTimeOffset.TryParse(ifModifiedSince, out var ifModifiedSinceDateTime) &&
            (ifModifiedSinceDateTime >= book.Modified))
        {
            return new StatusCodeResult(StatusCodes.Status304NotModified);
        }

        var bookViewModel = this.bookMapper.Map(book);
        httpContext.Response.Headers.LastModified = book.Modified.ToString("R", CultureInfo.InvariantCulture);
        return new OkObjectResult(bookViewModel);
    }

    public async Task<IActionResult> ExecuteSqlAsync(CancellationToken cancellationToken)
    {
        //await db.Connection.OpenAsync();
        var list = await this.bookRepository.GetBookSqlAsync(cancellationToken).ConfigureAwait(false);
        if (list is null)
        {
            return new NotFoundResult();
        }

        //var httpContext = this.actionContextAccessor.ActionContext!.HttpContext;
        //var ifModifiedSince = httpContext.Request.Headers.IfModifiedSince;
        //if (ifModifiedSince.Any() &&
        //    DateTimeOffset.TryParse(ifModifiedSince, out var ifModifiedSinceDateTime) &&
        //    (ifModifiedSinceDateTime >= book.Modified))
        //{
        //    return new StatusCodeResult(StatusCodes.Status304NotModified);
        //}

        //var bookViewModel = this.bookMapper.Map(book);
        //httpContext.Response.Headers.LastModified = book.Modified.ToString("R", CultureInfo.InvariantCulture);
        return new OkObjectResult(list);
    }
}
