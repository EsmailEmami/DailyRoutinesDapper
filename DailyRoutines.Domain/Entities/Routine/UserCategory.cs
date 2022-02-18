using DailyRoutines.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyRoutines.Domain.Entities.Routine
{
    public class UserCategory : BaseEntity
    {
        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public bool IsDelete { get; set; }

        #region Relations

        public User.User User { get; set; }
        public ICollection<Action> Actions { get; set; }

        #endregion
    }
}