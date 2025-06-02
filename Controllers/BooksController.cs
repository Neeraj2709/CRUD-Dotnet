using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookManagementAPI.Database;
using BookManagementAPI.DTOs;
using BookManagementAPI.Models;

namespace BookManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Books
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }

        // Get Book by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            return book != null ? Ok(book) : NotFound(new { message = "Book not found" });
        }

        // Get Books by Username
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetBooksByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return NotFound(new { message = "User not found" });

            var books = await _context.Books.Where(b => b.UserId == user.Id).ToListAsync();
            return Ok(books);
        }

        // Create Book
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookRequest bookRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == bookRequest.Username);
            if (user == null) return NotFound(new { message = "User not found" });

            var book = new Book
            {
                Title = bookRequest.Title,
                Author = bookRequest.Author,
                IsRead = bookRequest.IsRead,
                UserId = user.Id
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var bookResponse = new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsRead = book.IsRead,
                Username = user.Username
            };

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, bookResponse);
        }

        // Update Book
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookRequest bookRequest)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound(new { message = "Book not found" });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == bookRequest.Username);
            if (user == null || user.Id != book.UserId)
                return Unauthorized(new { message = "You are not authorized to update this book" });

            book.Title = bookRequest.Title;
            book.Author = bookRequest.Author;
            book.IsRead = bookRequest.IsRead;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        // Update Book Status
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookStatus(int id, [FromBody] BookRequest bookRequest)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound(new { message = "Book not found" });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == bookRequest.Username);
            if (user == null || user.Id != book.UserId)
                return Unauthorized(new { message = "You are not authorized to update this book status" });

            book.IsRead = bookRequest.IsRead;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        // Delete Book
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id, [FromQuery] string username)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound(new { message = "Book not found" });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || user.Id != book.UserId)
                return Unauthorized(new { message = "You are not authorized to delete this book" });

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Book deleted successfully" });
        }
    }
}
