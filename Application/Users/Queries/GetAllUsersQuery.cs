using BookManagementAPI.DTOs;
using MediatR;

namespace BookManagementAPI.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserResponse>>
    {
    }
}