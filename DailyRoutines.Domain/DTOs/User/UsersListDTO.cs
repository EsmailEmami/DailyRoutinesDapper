using System;

namespace DailyRoutines.Domain.DTOs.User;

public class UsersListDTO
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool IsBlock { get; set; }
}