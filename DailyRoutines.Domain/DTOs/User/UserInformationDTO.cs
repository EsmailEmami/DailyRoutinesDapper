using System;

namespace DailyRoutines.Domain.DTOs.User;

public class UserInformationDTO
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string CreateDate { get; set; }
    public bool IsBlock { get; set; }
}