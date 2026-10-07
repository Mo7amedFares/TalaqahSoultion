using Talaqah.Domain.Enums;

namespace Talaqah.Application.Features.Users.DTOs;

public class UserDto
{
    public int Id { get; init; }

    public string Email { get; init; } = "";

    public string FirstName { get; init; } = "";

    public string LastName { get; init; } = "";
    public string NationalId { get; init; } = "";

    public UserRole Role { get; init; }

    public UserType? UserType { get; init; }
}