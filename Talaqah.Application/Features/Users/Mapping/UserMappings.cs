using System.Linq.Expressions;
using Talaqah.Application.Features.Users.Commands.CreateUser;
using Talaqah.Application.Features.Users.DTOs;
using Talaqah.Domain.Entities;

namespace Talaqah.Application.Features.Users.Mapping;

public static class UserMappings
{
    // Query Mapping
    // Entity -> DTO
    // Used by EF Core Select()
    public static readonly Expression<Func<User, UserDto>> ToDto =
        user => new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            NationalId = user.NationalId,
            Role = user.Role,
            UserType = user.UserType
        };

    // Command Mapping
    // Command -> Entity
    // Used when creating a new User
    public static User ToEntity(
        this CreateUserCommand command)
    {
        return new User
        {
            Email = command.Email,
            PasswordHash = command.PasswordHash,
            FirstName = command.FirstName,
            LastName = command.LastName,
            NationalId = command.NationalID,
            Role = command.Role,
            UserType = command.UserType
        };
    }
}