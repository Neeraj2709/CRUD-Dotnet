using MediatR;
using BookManagementAPI.Database;
using BookManagementAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Users.Commands
{
    public class UpdateUsernameCommandHandler : IRequestHandler<UpdateUsernameCommand, UserResponse?>
    {
        private readonly ApplicationDbContext _context;

        public UpdateUsernameCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponse?> Handle(UpdateUsernameCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken);
            if (user == null) return null;

            user.Username = command.NewUsername;
            await _context.SaveChangesAsync(cancellationToken);

            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username
                // Map other properties as needed
            };
        }
    }
}