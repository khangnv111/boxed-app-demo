namespace BoxedApp.Controllers;

using Boxed.AspNetCore;
using BoxedApp.Commands;
using BoxedApp.Constants;
using BoxedApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Annotations;

[Route("[controller]")]
[ApiController]
[ApiVersion(ApiVersionName.V1)]
[SwaggerResponse(
    StatusCodes.Status500InternalServerError,
    "The MIME type in the Accept HTTP header is not acceptable.",
    typeof(ProblemDetails),
    ContentType.ProblemJson)]
public class BookController : ControllerBase
{
    //[HttpOptions(Name = BookControllerRoute.OptionsBook)]
    //[SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
    //public IActionResult Options()
    //{
    //    this.HttpContext.Response.Headers.AppendCommaSeparatedValues(
    //        HeaderNames.Allow,
    //        HttpMethods.Get,
    //        HttpMethods.Head,
    //        HttpMethods.Options,
    //        HttpMethods.Post);
    //    return this.Ok();
    //}

    [HttpGet("{bookId}", Name = BookControllerRoute.GetBook)]
    //[HttpHead("{bookId}", Name = BookControllerRoute.HeadBook)]
    [SwaggerResponse(
        StatusCodes.Status200OK,
        "The car with the specified unique identifier.",
        typeof(Book),
        ContentType.RestfulJson,
        ContentType.Json)]
    [SwaggerResponse(
        StatusCodes.Status304NotModified,
        "The car has not changed since the date given in the If-Modified-Since HTTP header.")]
    [SwaggerResponse(
        StatusCodes.Status404NotFound,
        "A car with the specified unique identifier could not be found.",
        typeof(ProblemDetails),
        ContentType.ProblemJson)]
    [SwaggerResponse(
        StatusCodes.Status406NotAcceptable,
        "The MIME type in the Accept HTTP header is not acceptable.",
        typeof(ProblemDetails),
        ContentType.ProblemJson)]
    public Task<IActionResult> GetAsync(
        [FromServices] GetBookCommand command,
        int bookId,
        CancellationToken cancellationToken) => command.ExecuteAsync(bookId, cancellationToken);


    [HttpGet("sql-booklist")]
    //[HttpHead("{bookId}", Name = BookControllerRoute.HeadBook)]
    //[SwaggerResponse(
    //    StatusCodes.Status200OK,
    //    "The car with the specified unique identifier.",
    //    typeof(Book),
    //    ContentType.RestfulJson,
    //    ContentType.Json)]
    //[SwaggerResponse(
    //    StatusCodes.Status304NotModified,
    //    "The car has not changed since the date given in the If-Modified-Since HTTP header.")]
    //[SwaggerResponse(
    //    StatusCodes.Status404NotFound,
    //    "A car with the specified unique identifier could not be found.",
    //    typeof(ProblemDetails),
    //    ContentType.ProblemJson)]
    //[SwaggerResponse(
    //    StatusCodes.Status406NotAcceptable,
    //    "The MIME type in the Accept HTTP header is not acceptable.",
    //    typeof(ProblemDetails),
    //    ContentType.ProblemJson)]
    public Task<IActionResult> GetSQLAsync(
        [FromServices] GetBookCommand command,
        CancellationToken cancellationToken) => command.ExecuteSqlAsync(cancellationToken);

    [HttpGet("bookstoreList")]
    //[HttpHead("", Name = CarsControllerRoute.HeadCarPage)]
    //[SwaggerResponse(
    //    StatusCodes.Status200OK,
    //    "A collection of cars for the specified page.",
    //    typeof(Connection<Car>),
    //    ContentType.RestfulJson,
    //    ContentType.Json)]
    //[SwaggerResponse(
    //    StatusCodes.Status400BadRequest,
    //    "The page request parameters are invalid.",
    //    typeof(ProblemDetails),
    //    ContentType.ProblemJson)]
    //[SwaggerResponse(
    //    StatusCodes.Status404NotFound,
    //    "A page with the specified page number was not found.",
    //    typeof(ProblemDetails),
    //    ContentType.ProblemJson)]
    //[SwaggerResponse(
    //    StatusCodes.Status406NotAcceptable,
    //    "The MIME type in the Accept HTTP header is not acceptable.",
    //    typeof(ProblemDetails),
    //    ContentType.ProblemJson)]
    public Task<IActionResult> GetPageAsync(
        [FromServices] BookStoreCommand command,
        CancellationToken cancellationToken) => command.ExecuteAsync(cancellationToken);

    
}
