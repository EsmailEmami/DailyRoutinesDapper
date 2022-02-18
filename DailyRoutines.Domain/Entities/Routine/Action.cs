using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DailyRoutines.Domain.Entities.Common;

namespace DailyRoutines.Domain.Entities.Routine
{
    public class Action : BaseEntity
    {
        [Display(Name = "عنوان فعالیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ActionTitle { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(3000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ActionDescription { get; set; }

        [ForeignKey("UserCategory")]
        public Guid UserCategoryId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public int CreatePersianYear { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public int CreatePersianMonth { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public int CreatePersianDay { get; set; }

        #region Relations

        public UserCategory UserCategory { get; set; }

        #endregion
    }
}