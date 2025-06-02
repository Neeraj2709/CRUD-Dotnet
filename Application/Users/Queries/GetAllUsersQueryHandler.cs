using BookManagementAPI.Database;
using BookManagementAPI.DTOs;
using BookManagementAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookManagementAPI.Application.Users.Queries;

namespace BookManagementAPI.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllUsersQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);

            var response = new List<UserResponse>();
            foreach (var user in users)
            {
                response.Add(new UserResponse
                {
                    Id = user.Id,
                    Username = user.Username
                    // Map other properties if needed
                });
            }

            return response;
        }
    }
}