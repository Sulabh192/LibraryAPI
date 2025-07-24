using Library.Data;
using Library.Models;
using Microsoft.Extensions.Logging;

namespace Library.Services;
using Microsoft.EntityFrameworkCore;
//This class implements the IBookService interface and uses the logic to interact with the database.
public class BookService : IBookService
{
    //Thi is the database context injected via DI.
    private readonly BookCatalogContext context;
    //This is the logger which tracks service actions.
    private readonly ILogger<BookService> logger;
    //This is a constructor that sets the context and logger via dependency injection.
    public BookService(BookCatalogContext context, ILogger<BookService> logger)
    {
        this.context = context;
        this.logger = logger;
    }
    //Gets all the books from the database.
    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await context.Books.ToListAsync(); //Converts the books to a list.
    }
    //Gets a specific book from a ID.  
    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await context.Books.FindAsync(id); //Returns the book with a specific ID.
    }
    //Search for books through its title or author.
    public async Task<IEnumerable<Book>> SearchBooksAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<Book>();
        //Make the string lowercase so that the title or author is easy to find.
        query = query.ToLowerInvariant();
        //Filter through the books by finding the authors name or the title of the book.
        return await context.Books
            .Where(b => b.Title.ToLower().Contains(query) || b.Author.ToLower().Contains(query))
            .ToListAsync();
    }
    //Add a new book using the createbook data transfer object.
    public async Task<Book> AddBookAsync(CreateBook dto, CancellationToken cancellationToken)
    {
        //Create a new book using the data from the DTO.
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            ISBN = dto.ISBN,
            PublishedOn = dto.PublishedOn
        };
        //Add the new book to the context.
        context.Books.Add(book);
        //Save the changes.
        await context.SaveChangesAsync(cancellationToken);
        //Log the action that is taking place.
        logger.LogInformation("Added new book: {Title}", book.Title);

        return book;
    }
    //Update the book using the ID and the UpdateBook data transfer object.
    public async Task<bool> UpdateBookAsync(int id, UpdateBook dto)
    {
        var book = await context.Books.FindAsync(id); // find the book to update.
        if (book == null)
            return false;
        //Update the fields to what the user wants to update it to.
        book.Title = dto.Title;
        book.Author = dto.Author;
        book.ISBN = dto.ISBN;
        book.PublishedOn = dto.PublishedOn;
        //Save the changes.
        await context.SaveChangesAsync();
        //Log the action that is being taken place.
        logger.LogInformation("Updated book id {Id}", id);

        return true;
    }
    //Delete a book by its ID.
    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await context.Books.FindAsync(id); //Find the book to delete.
        if (book == null)
            return false;

        context.Books.Remove(book); //Remove the book from the database.
        await context.SaveChangesAsync(); //Save the changes.

        logger.LogInformation("Deleted book id {Id}", id); //Log the action that is being taken place.
        return true;
    }
}