namespace Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.Services;
//This is an Web API controller.
[ApiController]
//This is the route for the API controller.
[Route("api/[controller]")]
public class BooksControllers : ControllerBase
{
    //This is the dependency injection for the book_service.
    private readonly IBookService book_service;
    //Constructer that sets the variables via dependency injection.
    public BooksControllers(IBookService bookService)
    {
        this.book_service = bookService;
    }
    // GET: api/books
    [HttpGet]
    //Retrieves all the books from the system.
    public async Task<IActionResult> GetAll()
    {
        var books = await book_service.GetAllBooksAsync(); //Calls the service to get all the books.
        return Ok(books);
    }
    // GET: api/books/{id}
    [HttpGet("{id:int}")]
    //Retrieves a specifc book by its ID.
    public async Task<IActionResult> GetById(int id)
    {
        var book = await book_service.GetBookByIdAsync(id); //Calls the service to get all books by its ID.
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }
    // GET: api/books/search?q=term
    [HttpGet("search")]
    //Searches for a book which matches its query string.
    public async Task<IActionResult> Search([FromQuery] string q)
    {
        var results = await book_service.SearchBooksAsync(q); //Searches using a query string.
        return Ok(results);
    }
    // POST: api/books
    [HttpPost]
    //Creates a new book based on the provided data.
    public async Task<IActionResult> Create([FromBody] CreateBook dto)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        var createdBook = await book_service.AddBookAsync(dto, HttpContext.RequestAborted); //Creates the book.
        return CreatedAtAction(nameof(GetById), new { id = createdBook.id }, createdBook);
    }
    // PUT: api/books/{id}
    [HttpPut("{id:int}")]
    //Updates a book based on the ID.
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBook dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await book_service.UpdateBookAsync(id, dto); //Updates the book or attempts to do so.
        if (!updated)
            return NotFound();

        return NoContent();
    }
// DELETE: api/books/{id}
    [HttpDelete("{id:int}")]
    //Deletes the book based on the ID.
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await book_service.DeleteBookAsync(id); //Deleets the book or attempts to delete it.
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

}