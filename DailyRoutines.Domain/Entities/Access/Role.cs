using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyRoutines.Domain.Entities.Access
{
    public class Role 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid roleId { get; set; }

        [Display(Name = "عنوان مقام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleName { get; set; }
    }
}