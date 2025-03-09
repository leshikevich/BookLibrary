using Library.BookLibrary.Impl;
using Library.BookLibrary.Models;

var library = new BookLibrary();

// Add books
library.AddBook(new Book("The Shining", "Stephen King", 447));
library.AddBook(new Book("Carrie", "Stephen King", 199));
library.AddBook(new Book("The Little Mermaid", "Hans Christian Andersen", 50));
library.AddBook(new Book("The Ugly Duckling", "Hans Christian Andersen", 30));

// Sort books
library.SortBooks();

// Display sorted books
Console.WriteLine("Sorted Books:");
foreach (var book in library.SearchByTitle("")) // Get all books
{
    Console.WriteLine($"{book.Author} - {book.Title} ({book.Pages} pages)");
}

// Search books by title
Console.WriteLine("\nSearch for 'The':");
List<Book> searchResults = library.SearchByTitle("The");
foreach (var book in searchResults)
{
    Console.WriteLine($"{book.Author} - {book.Title}");
}

// Save and Load from XML
string filePath = "books.xml";
library.SaveToXml(filePath);
Console.WriteLine($"\nBooks saved to {filePath}");

// Load books from file
var newLibrary = new BookLibrary();
var books = await newLibrary.LoadFromXmlAsync(filePath);
Console.WriteLine("\nBooks loaded from XML:");
foreach (var book in books)
{
    Console.WriteLine($"{book.Author} - {book.Title} ({book.Pages} pages)");
}