using Library.BookLibrary.Impl;
using Library.BookLibrary.Models;

namespace BookLibraryTests
{
    public class BookLibraryTests
    {
        [Fact]
        public void AddBook_ShouldAddBookToList()
        {
            var library = new BookLibrary();
            var book = new Book("Test Title", "Test Author", 123);

            library.AddBook(book);

            var books = library.SearchByTitle("Test Title");
            Assert.Single(books);
            Assert.Equal("Test Author", books[0].Author);
        }

        [Fact]
        public void SortBooks_ShouldSortByAuthorAndTitle()
        {
            var library = new BookLibrary();
            library.AddBook(new Book("B Title", "Author B", 200));
            library.AddBook(new Book("A Title", "Author B", 150));
            library.AddBook(new Book("Z Title", "Author A", 100));

            library.SortBooks();

            var books = library.SearchByTitle("");
            Assert.Equal("Author A", books[0].Author);
            Assert.Equal("Z Title", books[0].Title);
            Assert.Equal("Author B", books[1].Author);
            Assert.Equal("A Title", books[1].Title);
        }

        [Fact]
        public void SearchByTitle_ShouldReturnMatchingBooks()
        {
            var library = new BookLibrary();
            library.AddBook(new Book("C# Programming", "John Doe", 300));
            library.AddBook(new Book("Learn C#", "Jane Doe", 250));

            var results = library.SearchByTitle("C#");

            Assert.Equal(2, results.Count);
        }

        [Fact]
        public async Task SaveAndLoadFromXml_ShouldPreserveBooks()
        {
            var library = new BookLibrary();
            library.AddBook(new Book("XML Book", "XML Author", 400));
            string filePath = "test_books.xml";

            library.SaveToXml(filePath);

            var newLibrary = new BookLibrary();
            await newLibrary.LoadFromXmlAsync(filePath);

            var books = newLibrary.SearchByTitle("XML Book");
            Assert.Single(books);
            Assert.Equal("XML Author", books[0].Author);

            File.Delete(filePath);
        }

        [Fact]
        public async Task LoadFromXmlAsync_ShouldLoadBooksCorrectly()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "test_books.xml");
            string xmlContent = @"
                <Books>
                    <Book>
                        <Title>Book 1</Title>
                        <Author>Author A</Author>
                        <Pages>200</Pages>
                    </Book>
                    <Book>
                        <Title>Book 2</Title>
                        <Author>Author B</Author>
                        <Pages>300</Pages>
                    </Book>
                </Books>";

            await File.WriteAllTextAsync(filePath, xmlContent);

            var bookLibrary = new BookLibrary();
            List<Book> books = await bookLibrary.LoadFromXmlAsync(filePath);

            Assert.Equal(2, books.Count);
            Assert.Equal("Book 1", books[0].Title);
            Assert.Equal("Author A", books[0].Author);
            Assert.Equal(200, books[0].Pages);

            Assert.Equal("Book 2", books[1].Title);
            Assert.Equal("Author B", books[1].Author);
            Assert.Equal(300, books[1].Pages);

            File.Delete(filePath);
        }
    }
}