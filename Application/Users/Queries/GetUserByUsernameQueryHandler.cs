using BookManagementAPI.Database;
using BookManagementAPI.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BookManagementAPI.Users.Queries
{
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserResponse?>
    {
        private readonly ApplicationDbContext _context;

        public GetUserByUsernameQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponse?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

            if (user == null)
                return null;

            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username
                
                // Map other properties if needed
            };
        }
    }
}