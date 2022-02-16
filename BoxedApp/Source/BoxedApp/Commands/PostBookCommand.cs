namespace BoxedApp.Commands;

using Boxed.Mapping;
using BoxedApp.Constants;
using BoxedApp.Repositories;
using BoxedApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class PostBookCommand
{
    private readonly IBookRepository bookRepository;
    private readonly IMapper<Models.Book, Book> bookToMapper;
    private readonly IMapper<SaveBook, Models.Book> saveBookMapper;

    public PostBookCommand(
        IBookRepository bookRepository,
        IMapper<Models.Book, Book> bookToMapper,
        IMapper<SaveBook, Models.Book> saveBookMapper)
    {
        this.bookRepository = bookRepository;
        this.bookToMapper = bookToMapper;
        this.saveBookMapper = saveBookMapper;
    }

    public async Task<IActionResult> ExecuteAsync(SaveBook saveBook, CancellationToken cancellationToken)
    {
        var book = this.saveBookMapper.Map(saveBook);
        book = await this.bookRepository.AddAsync(book, cancellationToken).ConfigureAwait(false);
        var bViewModel = this.bookToMapper.Map(book);

        return new CreatedAtRouteResult(
            BookControllerRoute.GetBook,
            new { bookId = bViewModel.Id },
            bViewModel);
    }
}
