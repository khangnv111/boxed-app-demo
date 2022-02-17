namespace BoxedApp;

using BoxedApp.Commands;
using BoxedApp.Mappers;
using BoxedApp.Repositories;
using BoxedApp.Services;
using BoxedApp.ViewModels;
using Boxed.Mapping;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// <see cref="IServiceCollection"/> extension methods add project services.
/// </summary>
/// <remarks>
/// AddSingleton - Only one instance is ever created and returned.
/// AddScoped - A new instance is created and returned for each request/response cycle.
/// AddTransient - A new instance is created and returned each time.
/// </remarks>
internal static class ProjectServiceCollectionExtensions
{
    public static IServiceCollection AddProjectCommands(this IServiceCollection services) =>
        services
            .AddSingleton<DeleteCarCommand>()
            .AddSingleton<GetCarCommand>()
            .AddSingleton<GetCarPageCommand>()
            .AddSingleton<PatchCarCommand>()
            .AddSingleton<PostCarCommand>()
            .AddSingleton<PutCarCommand>()
        //book
        .AddSingleton<GetBookCommand>()
        .AddSingleton<PostBookCommand>()
        //bookstore
        .AddSingleton<BookStoreCommand>();

    public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
        services
            .AddSingleton<IMapper<Models.Car, Car>, CarToCarMapper>()
            .AddSingleton<IMapper<Models.Car, SaveCar>, CarToSaveCarMapper>()
            .AddSingleton<IMapper<SaveCar, Models.Car>, CarToSaveCarMapper>()
        //book
        .AddSingleton<IMapper<Models.Book, Book>, BookToBookMapper>()
        .AddSingleton<IMapper<Models.Book, SaveBook>, BookToSaveBookMapper>()
        .AddSingleton<IMapper<SaveBook, Models.Book>, BookToSaveBookMapper>()
        //bookstore
        .AddSingleton<IMapper<Models.BookStore, Models.BookStore>, BookStoreMapper>();

    public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
        services
            .AddSingleton<ICarRepository, CarRepository>()
            .AddSingleton<IBookRepository, BookRepository>()
            //
            /*.AddScoped<ISqlRepository, SqlRepository>()*/;

    public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
        services
            .AddSingleton<IClockService, ClockService>()
            /*.AddSingleton<MongoDBService>()*/;
}
