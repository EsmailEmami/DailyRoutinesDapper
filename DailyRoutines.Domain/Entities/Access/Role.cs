using DailyRoutines.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace DailyRoutines.Domain.Entities.Access
{
    public class Role : BaseEntity
    {
        [Display(Name = "عنوان مقام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleName { get; set; }
    }
}