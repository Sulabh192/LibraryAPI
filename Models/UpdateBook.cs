namespace Library.Models;
//This is an immutable data transfer object used to update a new book.
public record UpdateBook(
    string Title,
    string Author,
    string ISBN,
    DateTime PublishedOn
    );