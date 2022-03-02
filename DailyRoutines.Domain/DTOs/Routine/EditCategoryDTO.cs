using System;
using System.ComponentModel.DataAnnotations;

namespace DailyRoutines.Domain.DTOs.Routine;

public class EditCategoryDTO
{
    [Required]
    public Guid CategoryId { get; set; }

    [Display(Name = "عنوان دسته بندی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string CategoryTitle { get; set; }
}