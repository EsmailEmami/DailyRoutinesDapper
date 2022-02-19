using System;

namespace DailyRoutines.Domain.DTOs.User;

public class UsersListDTO
{
    public UsersListDTO(Guid userId, string fullName, string phoneNumber, string email, bool isBlock)
    {
        UserId = userId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Email = email;
        IsBlock = isBlock;
    }

    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool IsBlock { get; set; }
}