namespace Library.Models;
//This class creates the book and sets the ID, Title, Author, ISBN, and DateTime.
public class Book
{
    public int id { get; set; }                  
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public DateTime PublishedOn { get; set; }
}