using System.ComponentModel.DataAnnotations;

namespace DailyRoutines.Domain.DTOs.User;

public class EditUserDashboardDTO
{
    [Display(Name = "نام")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string LastName { get; set; }
}