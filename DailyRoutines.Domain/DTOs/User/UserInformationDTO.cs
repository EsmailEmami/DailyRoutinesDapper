using System;

namespace DailyRoutines.Domain.DTOs.User;

public class UserInformationDTO
{
    public UserInformationDTO(Guid userId, string firstName, string lastName, string phoneNumber, string email, string createDate, bool isBlock)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        CreateDate = createDate;
        IsBlock = isBlock;
    }


    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string CreateDate { get; set; }
    public bool IsBlock { get; set; }
}