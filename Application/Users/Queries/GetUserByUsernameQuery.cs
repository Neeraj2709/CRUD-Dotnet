using BookManagementAPI.DTOs;
using MediatR;

namespace BookManagementAPI.Users.Queries
{
    public class GetUserByUsernameQuery : IRequest<UserResponse?>
    {
        public string Username { get; }

        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }
    }
}