using System;
using System.ComponentModel.DataAnnotations;

namespace DailyRoutines.Domain.DTOs.Access;

public class EditRoleDTO
{
    [Required]
    public Guid RoleId { get; set; }

    [Display(Name = "عنوان مقام")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string RoleName { get; set; }
}