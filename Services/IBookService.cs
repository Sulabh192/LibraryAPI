using Library.Models;

namespace Library.Services;
//This is the interface for book related service operations.
public interface IBookService
{
    //This method gets all the books.
    Task<IEnumerable<Book>> GetAllBooksAsync();
    //This method gets the books by its ID.
    Task<Book> GetBookByIdAsync(int id);
    //This method searches the books by its title or author.
    Task<IEnumerable<Book>> SearchBooksAsync(string query);
    //This method adds a new book.
    Task<Book> AddBookAsync(CreateBook dto, CancellationToken cancellationToken);
    //This method updates an exisiting book.
    Task<bool> UpdateBookAsync(int id, UpdateBook dto);
    //This method deletes a book by its ID.
    Task<bool> DeleteBookAsync(int id);
}