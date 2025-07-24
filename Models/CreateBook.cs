namespace Library.Models;
//This is an immutable data transfer object used to create a new book.
public record CreateBook
(
    string Title,
    string Author,
    string ISBN, 
    DateTime PublishedOn
);