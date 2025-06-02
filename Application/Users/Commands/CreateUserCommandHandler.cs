using BookManagementAPI.Application.Users.Commands;
using MediatR;
using BookManagementAPI.Database;
using BookManagementAPI.DTOs;
using BookManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly ApplicationDbContext _context;

        public CreateUserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = command.Request.Username
                // Add other properties as needed
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username
                
                // Map other properties
            };
        }
    }
}