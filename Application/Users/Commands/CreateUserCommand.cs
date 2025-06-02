using MediatR;
using BookManagementAPI.DTOs;

namespace BookManagementAPI.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<UserResponse>
    {
        public UserRequest Request { get; }

        public CreateUserCommand(UserRequest request)
        {
            Request = request;
        }
    }
}