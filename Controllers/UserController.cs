using BookManagementAPI.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookManagementAPI.Database;
using BookManagementAPI.Models;
using MediatR;

namespace BookManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public UsersController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;  
            _mediator = mediator; 
        }
    // Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return users.Any() ? Ok(users) : NotFound(new { message = "No users found." });
        }

    //Add User
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            try
            {
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                return Conflict(new { message = "Username already exists." });

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);
        }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while processing your request. Please try again later." });
            }
        }
        
        // Find User by Username

        [HttpGet("find/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user != null ? Ok(new { user.Id, user.Username }) : NotFound(new { message = "User not found" });
        }
        // Update Username
        [HttpPut("update/{currentUsername}/{newUsername}")]
        public async Task<IActionResult> UpdateUsername(string currentUsername, string newUsername)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == currentUsername);
            if (user == null) return NotFound(new { message = "User not found" });

            if (await _context.Users.AnyAsync(u => u.Username == newUsername))
                return Conflict(new { message = "Username already exists." });

            user.Username = newUsername;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Username updated successfully", user });
        }
        // Delete User
        [HttpDelete("delete/{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return NotFound(new { message = "User not found" });

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
