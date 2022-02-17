using Boxed.AspNetCore;
using BoxedApp.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BoxedApp.IntegrationTest
{
    public class BooksControllerTest : CustomWebApplicationFactory<Program>
    {
        private readonly HttpClient client;
        private readonly MediaTypeFormatterCollection formatters;

        public BooksControllerTest()
        {
            this.client = this.CreateClient();
            this.formatters = new MediaTypeFormatterCollection();
            this.formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(ContentType.RestfulJson));
            this.formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(ContentType.ProblemJson));
        }

        [Fact]
        public async Task Get_BookFound_Returns200OkAsync()
        {
            //var book = new Models.BookDB() {};
            //this.BookRepositoryMock.Setup(x => x.GetAsync(1, It.IsAny<CancellationToken>()));

            var response = await this.client.GetAsync(new Uri("/book/1", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType?.MediaType);
            //var bookViewModel = await response.Content.ReadAsAsync<BookDB>(this.formatters).ConfigureAwait(false);
        }

        [Fact]
        public async Task Get_BookSQLList()
        {
            //var list = new List<BookDB>()
            //{
            //    new BookDB(){Id = 1, NameBook="book1"},
            //    new BookDB(){Id = 2, NameBook = "book2"},
            //    new BookDB(){Id = 3,NameBook = "book3"},
            //};
            //this.BookRepositoryMock.Setup(x => x.GetBookSqlAsync(It.IsAny<CancellationToken>()));

            var response = await this.client.GetAsync(new Uri("/book/sql-booklist", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType?.MediaType);
            //var list = await response.Content.ReadAsAsync<List<Book>>(this.formatters).ConfigureAwait(false);
        }

        [Fact]
        public async Task Get_BookMongoList()
        {
            var response = await this.client.GetAsync(new Uri("/book/bookstoreList", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType?.MediaType);
            //var list = await response.Content.ReadAsAsync<List<BookStore>>(this.formatters).ConfigureAwait(false);
        }

        [Theory]
        [InlineData("/book/sql-booklist")]
        [InlineData("/book/sql-booklist?count=2")]
        public async Task Get_BookSQLListBuyCount(string path)
        {
            //var list = new List<BookDB>()
            //{
            //    new BookDB(){Id = 1, NameBook="book1"},
            //    new BookDB(){Id = 2, NameBook = "book2"},
            //    new BookDB(){Id = 3,NameBook = "book3"},
            //};
            //this.BookRepositoryMock.Setup(x => x.GetBookSqlAsync(It.IsAny<CancellationToken>()));

            var response = await this.client.GetAsync(new Uri(path, UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType?.MediaType);
            
            var list = await response.Content.ReadAsAsync<List<Book>>(this.formatters).ConfigureAwait(false);

            Assert.Equal(2, list.Count);
        }
    }
}
