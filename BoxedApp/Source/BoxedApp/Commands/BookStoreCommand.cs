namespace BoxedApp.Commands;

using BoxedApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public class BookStoreCommand
{
    private readonly IActionContextAccessor actionContextAccessor;
    private readonly IBookRepository bookRepository;

    public BookStoreCommand(
        IActionContextAccessor actionContextAccessor,
        IBookRepository bookRepository)
    {
        this.actionContextAccessor = actionContextAccessor;
        this.bookRepository = bookRepository;
    }

    public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
    {
        var list = await this.bookRepository.GetBookStoreAsync(cancellationToken).ConfigureAwait(false);

        return new OkObjectResult(list);
    }
}
