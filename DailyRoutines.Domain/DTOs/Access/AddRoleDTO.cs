using System.ComponentModel.DataAnnotations;

namespace DailyRoutines.Domain.DTOs.Access;

public class AddRoleDTO
{
    [Display(Name = "عنوان مقام")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string RoleName { get; set; }
}