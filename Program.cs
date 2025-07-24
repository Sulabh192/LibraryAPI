using Library.Data;
using Library.Middleware;
using Library.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//Creates a Web application.
var builder = WebApplication.CreateBuilder(args);
//Configures the logging.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
//Register EF Core with an in-memory database called "BookCatalog"
builder.Services.AddDbContext<BookCatalogContext>(options => options.UseInMemoryDatabase("BookCatalog"));
// Register the book service with scoped lifetime
builder.Services.AddScoped<IBookService, BookService>();
//Adds a controller
builder.Services.AddControllers();
//Adds support for minimal API exploration.
builder.Services.AddEndpointsApiExplorer();
// Adds a Swagger for API documentation/testing/
builder.Services.AddSwaggerGen();
// Build the app using the configured builder
var app = builder.Build();
// Register custom global error-handling middleware
app.UseMiddleware<Middleware>();
// Enable Swagger middleware to generate the OpenAPI spec
app.UseSwagger();
// Enable Swagger UI for testing API endpoints interactively
app.UseSwaggerUI();
// Redirect HTTP requests to HTTPS automatically
app.UseHttpsRedirection();
// Map HTTP requests to controller actions based on routes
app.MapControllers();
// Run the application
app.Run();