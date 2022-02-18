using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyRoutines.Domain.Entities.Common
{
    public class BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "تاریخ ساخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
    }
}