using System;
using System.ComponentModel.DataAnnotations;

namespace DailyRoutines.Domain.DTOs.Routine;

public class EditActionDTO
{
    public EditActionDTO(Guid actionId, string actionTitle, string actionDescription)
    {
        ActionId = actionId;
        ActionTitle = actionTitle;
        ActionDescription = actionDescription;
    }


    [Required]
    public Guid ActionId { get; set; }


    [Display(Name = "عنوان فعالیت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string ActionTitle { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    [MaxLength(3000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string ActionDescription { get; set; }
}