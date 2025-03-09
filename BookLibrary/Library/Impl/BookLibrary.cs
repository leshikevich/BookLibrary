using Library.BookLibrary.Interfaces;
using Library.BookLibrary.Models;
using System.Xml.Linq;

namespace Library.BookLibrary.Impl
{
    public class BookLibrary : IBookLibrary
    {
        private List<Book> books = new();

        public async Task<List<Book>> LoadFromXmlAsync(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("XML file not found.");

            XDocument doc = await Task.Run(() => XDocument.Load(filePath));

            books = doc.Root.Elements("Book")
                            .Select(b => new Book(
                                b.Element("Title")?.Value ?? "Unknown Title",
                                b.Element("Author")?.Value ?? "Unknown Author",
                                int.TryParse(b.Element("Pages")?.Value, out var result) ? result : 0))
                            .ToList();

            return books;
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void SortBooks()
        {
            books = books.OrderBy(b => b.Author).ThenBy(b => b.Title).ToList();
        }

        public List<Book> SearchByTitle(string keyword)
        {
            return books.Where(b => b.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public void SaveToXml(string filePath)
        {
            XDocument doc = new XDocument(
                new XElement("Books",
                    books.Select(b => new XElement("Book",
                        new XElement("Title", b.Title),
                        new XElement("Author", b.Author),
                        new XElement("Pages", b.Pages))
                    )
                )
            );
            doc.Save(filePath);
        }
    }
}
