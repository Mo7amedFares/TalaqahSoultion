using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Users.DTOs;
using Talaqah.Domain.Enums;

namespace Talaqah.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Email,
    string PasswordHash,
    string FirstName,
    string LastName,
    string NationalID,
    UserRole Role,
    UserType? UserType
) : IRequest<Result<UserDto>>;
