using Library.BookLibrary.Models;

namespace Library.BookLibrary.Interfaces
{
    interface IBookLibrary
    {
        Task<List<Book>> LoadFromXmlAsync(string filePath);
        void AddBook(Book book);
        void SortBooks();
        List<Book> SearchByTitle(string keyword);
        void SaveToXml(string filePath);
    }
}
